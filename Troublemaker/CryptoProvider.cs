using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace Troublemaker.Unpacker
{
    public sealed class CryptoProvider : IDisposable
    {
        private readonly AesCryptoServiceProvider _aes;

        public CryptoProvider(Byte[] key, Byte[] iv)
        {
            _aes = InitCryptoServiceProvider(key, iv);
        }

        public void Dispose()
        {
            _aes?.Dispose();
        }

        private AesCryptoServiceProvider InitCryptoServiceProvider(Byte[] key, Byte[] iv)
        {
            var provider = new AesCryptoServiceProvider();
            provider.Mode = CipherMode.CBC;
            provider.Padding = PaddingMode.Zeros;
            provider.KeySize = key.Length * 8;
            provider.Key = key;
            provider.IV = iv;
            return provider;
        }

        public XmlDocument DecryptXml(String filePath)
        {
            using var input = DecryptFile(filePath, PackageCompressionType.Encrypted);
            using var sr = new StreamReader(input);

            var xml = sr.ReadToEnd();
            xml = xml.TrimEnd('\0');

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            
            return doc;
        }

        public Stream DecryptFile(String inputPath, PackageCompressionType compressionType)
        {
            Stream inputStream = File.OpenRead(inputPath);
            if (compressionType == PackageCompressionType.Raw)
                return inputStream;

            using var builder = new StreamBuilder();

            if (compressionType == PackageCompressionType.Encrypted || compressionType == PackageCompressionType.EncryptedZip)
            {
                builder.Push(inputStream);
                ICryptoTransform decryptor = builder.Push(_aes.CreateDecryptor());
                inputStream = new CryptoStream(inputStream, decryptor, CryptoStreamMode.Read);
            }

            if (compressionType == PackageCompressionType.Zip || compressionType == PackageCompressionType.EncryptedZip)
            {
                builder.Push(inputStream);
                ZipArchive archive = builder.Push(new ZipArchive(inputStream));
                ZipArchiveEntry entry = archive.Entries.Single();
                inputStream = entry.Open();
            }

            return builder.Commit(inputStream);
        }
    }

    public sealed class StreamBuilder : IDisposable
    {
        private readonly LinkedList<IDisposable> _queue = new LinkedList<IDisposable>();

        public T Push<T>(T dependency) where T : IDisposable
        {
            _queue.AddLast(dependency);
            return dependency;
        }

        public Stream Commit(Stream stream)
        {
            DependentStream result = new DependentStream(stream);

            foreach (var item in _queue)
                result.AddStreamDependency(item);

            _queue.Clear();
            return result;
        }

        public void Dispose()
        {
            LinkedListNode<IDisposable> last = _queue.Last;
            while (last != null)
            {
                last.Value.Dispose();
                last = last.Previous;
            }
            _queue.Clear();
        }
    }
}