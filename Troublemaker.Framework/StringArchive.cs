using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Troublemaker.Framework
{
    public sealed class StringArchive
    {
        private const Int32 ParallelThreshold = 8;
        
        public static Encoding ArchiveEncoding => Encoding.UTF8;
        public static Encoding ContentEncoding => Encoding.Unicode;

        public String ArchivePath { get; }

        public StringArchive(String archivePath)
        {
            ArchivePath = archivePath;
        }

        private static readonly HashSet<String> _backuped = new HashSet<String>();

        public void MakeBackup()
        {
            if (!_backuped.Add(ArchivePath))
                return;

            if (File.Exists(ArchivePath))
            {
                String backupPath = Path.ChangeExtension(ArchivePath, $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.zip");
                File.Copy(ArchivePath, backupPath);
            }
        }

        public void Write(String name, String content)
        {
            MakeBackup();
            
            name = GetUniqueName(name);

            using (var zip = OpenWrite())
            {
                ZipArchiveEntry entry = zip.GetEntry(name);
                
                if (entry is null)
                    entry = zip.CreateEntry(name);

                using (var output = entry.Open())
                {
                    using (var sw = new StreamWriter(output, ContentEncoding, leaveOpen: true))
                        sw.Write(content);

                    output.SetLength(output.Position);
                }

                entry.LastWriteTime = DateTimeOffset.Now;
            }
        }

        public String? TryRead(String name)
        {
            using (var zip = TryOpenRead())
                return zip is null ? null : TryRead(zip, name);
        }

        public Map<String> TryRead(IReadOnlyCollection<String> names)
        {
            var map = new Map<String>(names.Count);

            using (var zip = TryOpenRead())
            {
                if (zip is null)
                    return map;

                ReadSequentially(zip);
            }

            void ReadSequentially(ZipArchive zip)
            {
                foreach (var name in names)
                {
                    String? str = TryRead(zip, name);
                    if (str is null)
                        continue;

                    map.Add(name, str);
                }
            }

            return map;
        }

        public Map<String> TryReadDirectory(String directoryPath)
        {
            using (var zip = TryOpenRead())
            {
                if (zip is null)
                    return new Map<String>(0);

                ZipArchiveEntry[] entries = zip.Entries.Where(e => e.FullName.StartsWith(directoryPath)).ToArray();
                var map = new Map<String>(entries.Length);

                foreach (ZipArchiveEntry entry in entries)
                {
                    String name = entry.FullName.Substring(directoryPath.Length);
                    name = TrimUniqueId(name);
                    map.Add(name, Read(entry));
                }

                return map;
            }
        }

        private ZipArchive? TryOpenRead()
        {
            if (!File.Exists(ArchivePath))
                return null;

            var input = File.OpenRead(ArchivePath);
            try
            {
                return new ZipArchive(input, ZipArchiveMode.Read, leaveOpen: false, ArchiveEncoding);
            }
            catch
            {
                input.Dispose();
                throw;
            }
        }

        private ZipArchive OpenWrite()
        {
            var input = File.Open(ArchivePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            try
            {
                return new ZipArchive(input, ZipArchiveMode.Update, leaveOpen: false, ArchiveEncoding);
            }
            catch
            {
                input.Dispose();
                throw;
            }
        }

        private static String? TryRead(ZipArchive zip, String name)
        {
            if (!TryFind(zip, name, out var entry))
                return null;

            return Read(entry);
        }

        private static Boolean TryFind(ZipArchive zip, String name, out ZipArchiveEntry entry)
        {
            name = GetUniqueName(name);

            entry = zip.GetEntry(name);
            return !(entry is null);
        }

        private static String Read(ZipArchiveEntry entry)
        {
            using (var content = entry.Open()) return ReadAsString(content);
        }

        private static String ReadAsString(Stream content)
        {
            using (var sr = new StreamReader(content, ContentEncoding))
                return sr.ReadToEnd();
        }

        private static String GetUniqueName(String name)
        {
            return name + "_0x" + name.GetDeterministicHashCode().ToString("X8");
        }

        private static String TrimUniqueId(String name)
        {
            return name.Substring(0, name.Length - "_0x00000000".Length);
        }
    }
}