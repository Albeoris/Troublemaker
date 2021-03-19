using System;
using System.Collections.Generic;
using System.Linq;

namespace Troublemaker.Framework
{
    public class Map<T> : Dictionary<String, T>, IEnumerable<T>
    {
        public Map()
        {
        }

        public Map(StringComparer? comparer = null)
            : base(comparer)
        {
        }

        public Map(Int32 capacity)
            : base(capacity)
        {
        }

        public Map(Int32 capacity, StringComparer comparer)
            : base(capacity, comparer)
        {
        }

        public  IReadOnlyDictionary<String, T> Pairs => this;

        public new IEnumerator<T> GetEnumerator()
        {
            return Values.GetEnumerator();
        }
    }
}