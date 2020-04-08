using System;
using System.Collections.Generic;

namespace Troublemaker.Framework
{
    public sealed class Cache<T> : Dictionary<String, T>
    {
        private readonly Func<String, T> _factory;

        public Cache(Func<String, T> factory)
            : base(StringComparer.Ordinal)
        {
            _factory = factory;
        }

        public T Ensure(String key)
        {
            return this.Ensure(key, _factory);
        }
    }
}