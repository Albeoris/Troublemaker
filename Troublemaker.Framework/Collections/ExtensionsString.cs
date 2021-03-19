using System;

namespace Troublemaker.Framework
{
    public static class ExtensionsString
    {
        public static String TrimPrefix(this String str, String prefix)
        {
            return str.StartsWith(prefix) ? str.Substring(prefix.Length) : str;
        }
        
        public static Int32 GetDeterministicHashCode(this String str)
        {
            unchecked
            {
                int hash1 = (5381 << 16) + 5381;
                int hash2 = hash1;

                for (int i = 0; i < str.Length; i += 2)
                {
                    hash1 = ((hash1 << 5) + hash1) ^ str[i];
                    if (i == str.Length - 1)
                        break;
                    hash2 = ((hash2 << 5) + hash2) ^ str[i + 1];
                }

                return hash1 + (hash2 * 1566083941);
            }
        }

    }
}