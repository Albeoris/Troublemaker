using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    public sealed class ExpandableEnd : IExpandable
    {
        public static IExpandable Instance { get; } = new ExpandableEnd();

        private ExpandableEnd()
        {
        }

        public String NodeName => throw new NotSupportedException();
        public IEnumerable<(String name, IExpandable expandable)> EnumerateChildren() => throw new NotSupportedException();
    }
}