using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    public sealed class ExpandableCollection : IExpandable
    {
        private readonly IEnumerable<IExpandable> _enumerable;

        public String NodeName { get; }

        public ExpandableCollection(String nodeName, IEnumerable<IExpandable> enumerable)
        {
            NodeName = nodeName;
            _enumerable = enumerable;
        }

        public IEnumerable<(String name, IExpandable expandable)> EnumerateChildren()
        {
            foreach (var expandable in _enumerable)
                yield return (expandable.NodeName, expandable);
        }
    }
}