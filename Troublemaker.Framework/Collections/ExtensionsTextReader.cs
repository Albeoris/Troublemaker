using System;
using System.IO;

namespace Troublemaker.Framework
{
    public static class ExtensionsTextReader
    {
        public static String RequireLine(this TextReader tr)
        {
            return tr.ReadLine() ?? throw new EndOfStreamException();
        }
    }
}