using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace Troublemaker.Unpacker
{
    public sealed class UnpackArguments
    {
        public Byte[] Key { get; }
        public Byte[] IV { get; }
        public String InputDirectory { get; }
        public String OutputDirectory { get; }
        public Boolean Move { get; }

        public UnpackArguments(String[] args)
        {
            foreach ((String key, String value) in EnumerateArguments(args))
            {
                try
                {
                    switch (key)
                    {
                        case "move":
                            Move = true;
                            break;
                        case "key":
                            Key = ParseBase64(value);
                            break;
                        case "iv":
                            IV = ParseBase64(value);
                            break;
                        case "input":
                            InputDirectory = OpenDirectory(value);
                            break;
                        case "output":
                            OutputDirectory = CreateDirectory(value);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"Failed to process argument [{key}] with value [{value}]", ex);
                }
            }

            if (Key == null) throw new ArgumentException("/key is not set.");
            if (IV == null) throw new ArgumentException("/iv is not set.");
            if (InputDirectory == null) throw new ArgumentException("/input is not set.");
            if (OutputDirectory == null) throw new ArgumentException("/output is not set.");
        }

        private String OpenDirectory(String value)
        {
            var directory = new DirectoryInfo(value);
            if (!directory.Exists)
                throw new DirectoryNotFoundException(value);
            return directory.FullName;
        }

        private String CreateDirectory(String value)
        {
            DirectoryInfo directory = Directory.CreateDirectory(value);
            return directory.FullName;
        }

        private Byte[] ParseBase64(String value)
        {
            return Convert.FromBase64String(value);
        }

        private static IEnumerable<(String key, String value)> EnumerateArguments(String[] args)
        {
            foreach (var arg in args)
            {
                if (arg[0] != '/' && arg[0] != '\\' && arg[0] != '-')
                    throw new NotSupportedException(arg);

                var index = arg.IndexOf(':');
                if (index < 1)
                    index = arg.Length;

                var key = arg.Substring(1, index - 1).ToLowerInvariant();
                var value = index == arg.Length ? null : arg.Substring(index + 1);

                yield return (key, value);
            }
        }
    }
}