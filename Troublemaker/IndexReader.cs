using System;
using System.Collections.Generic;
using System.Xml;

namespace Troublemaker.Unpacker
{
    public sealed class IndexReader
    {
        private readonly XmlDocument _doc;

        public IndexReader(XmlDocument doc)
        {
            _doc = doc;
        }

        public IReadOnlyList<IndexEntry> ReadAll()
        {
            XmlNode root = _doc.FirstChild;
            if (root.Name != "index")
                throw new NotSupportedException(root.OuterXml);

            Int32 index = 0;
            IndexEntry[] result = new IndexEntry[root.ChildNodes.Count];

            foreach (XmlElement child in root.ChildNodes)
                result[index++] = Parse(child);

            return result;
        }

        private IndexEntry Parse(XmlElement xml)
        {
            if (xml.Name != "entry")
                throw new NotSupportedException(xml.OuterXml);

            var relativePath = xml.GetAttribute("original");
            var packageRelativePath = xml.GetAttribute("pack");
            var size = Int64.Parse(xml.GetAttribute("size"));
            var compressedSize = Int64.Parse(xml.GetAttribute("csize"));
            var determineCompressionType = DetermineCompressionType(xml.GetAttribute("method"));

            PackageCompressionType DetermineCompressionType(String method)
            {
                switch (method)
                {
                    case "raw":
                        return PackageCompressionType.Raw;
                    case "zip":
                        return PackageCompressionType.Zip;
                    case "encrypted_zip":
                        return PackageCompressionType.EncryptedZip;
                    default:
                        throw new NotSupportedException(method);
                }
            }

            return new IndexEntry(relativePath, packageRelativePath, size, compressedSize, determineCompressionType);
        }
    }
}