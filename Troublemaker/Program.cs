using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using Troublemaker.Xml;

namespace Troublemaker.Unpacker
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                UnpackArguments arguments = new UnpackArguments(args);

                Unpack(arguments);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                Console.WriteLine();
                Console.WriteLine("Press Enter to exit...");
                Console.ReadLine();
            }
        }

        private static void Unpack(UnpackArguments arguments)
        {
            using var crypto = new CryptoProvider(arguments.Key, arguments.IV);

            IReadOnlyList<IndexEntry> entries = ReadIndexEntries(arguments, crypto);

            Int64 processedSize = 0;
            Int64 totalSize = entries.Sum(e => e.Size);

            Byte[] buff = new Byte[64 * 1024];
            foreach (var entry in entries)
            {
                String inputPath = Path.Combine(arguments.InputDirectory, entry.PackageRelativePath);
                String outputPath = Path.Combine(arguments.OutputDirectory, entry.RelativePath);

                if (!File.Exists(inputPath))
                    continue;

                String directoryPath = Path.GetDirectoryName(outputPath);
                Directory.CreateDirectory(directoryPath);

                using (var input = crypto.DecryptFile(inputPath, entry.CompressionType))
                using (var output = File.Create(outputPath))
                {
                    var size = entry.Size;
                    while (size > 0)
                    {
                        Int32 read = input.Read(buff, 0, buff.Length);
                        output.Write(buff, 0, read);
                        size -= read;
                        UpdateProcessedSize(in totalSize, ref processedSize, read);
                    }
                }
            }

            UpdateProcessedSize(in totalSize, ref processedSize, 0);
        }

        private static DateTime _lastProgressTime;

        private static Int64 UpdateProcessedSize(in Int64 totalSize, ref Int64 processedSize, Int32 readed)
        {
            Interlocked.Add(ref processedSize, readed);

            DateTime now = DateTime.UtcNow;
            if ((now - _lastProgressTime).Duration().TotalSeconds > 1)
            {
                _lastProgressTime = now;

                Single processed = processedSize / 1024f / 1024f;
                Single total = totalSize / 1024f / 1024f;
                Console.Title = $"Unpacking: {processed:F2} / {total:F2} MB";
            }

            return processedSize;
        }

        private static IReadOnlyList<IndexEntry> ReadIndexEntries(UnpackArguments arguments, CryptoProvider crypto)
        {
            String indexPath = Path.Combine(arguments.InputDirectory, "index");

            XmlDocument indexDoc = crypto.DecryptXml(indexPath);
            IndexReader reader = new IndexReader(indexDoc);

            return reader.ReadAll();
        }
    }
}