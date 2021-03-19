using System;
using System.Collections.Generic;

namespace Troublemaker.Framework
{
    public class SubstringComparer : IComparer<String>
    {
        private readonly StringComparison _comparison;

        public SubstringComparer(StringComparison comparison)
        {
            _comparison = comparison;
        }

        public Int32 Compare(String? str, String? substring)
        {
            if (!String.IsNullOrEmpty(str) && !String.IsNullOrEmpty(substring) && str.Length > substring.Length)
                str = str.Substring(0, substring.Length);

            return String.Compare(str, substring, _comparison);
        }
    }
}