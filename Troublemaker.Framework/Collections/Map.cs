using System;
using System.Collections.Generic;

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

        public new IEnumerator<T> GetEnumerator()
        {
            return Values.GetEnumerator();
        }
    }
}