using System;

namespace Troublemaker.Unpacker
{
    public sealed class IndexEntry
    {
        public String RelativePath { get; }
        public String PackageRelativePath { get; }
        public Int64 Size { get; }
        public Int64 CompressedSize { get; }
        public PackageCompressionType CompressionType { get; }

        public IndexEntry(String relativePath, String packageRelativePath, in Int64 size, in Int64 compressedSize, in PackageCompressionType compressionType)
        {
            RelativePath = relativePath;
            PackageRelativePath = packageRelativePath;
            Size = size;
            CompressedSize = compressedSize;
            CompressionType = compressionType;
        }
    }
}