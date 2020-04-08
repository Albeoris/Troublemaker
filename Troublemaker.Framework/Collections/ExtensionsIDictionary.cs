using System;
using System.Collections.Generic;

namespace Troublemaker.Framework
{
    public static class ExtensionsIDictionary
    {
        public static TValue Ensure<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key, Func<TValue> factory)
        {
            if (!dic.TryGetValue(key, out var value))
            {
                value = factory();
                dic[key] = value;
            }

            return value;
        }

        public static TValue Ensure<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key, Func<TKey, TValue> factory)
        {
            if (!dic.TryGetValue(key, out var value))
            {
                value = factory(key);
                dic[key] = value;
            }

            return value;
        }
    }
}
