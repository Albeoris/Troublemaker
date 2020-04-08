using System;
using System.Collections.Generic;
using System.IO;

namespace Troublemaker.Framework
{
    public static class ExtensionsLinkedList
    {
        public static T RequireLastValue<T>(this LinkedList<T> ll)
        {
            var last = ll.Last ?? throw new InvalidOperationException("List is empty.");
            return last.Value;
        }

        public static LinkedListNode<T> Get<T>(this LinkedList<T> ll, T value)
        {
            return ll.Find(value) ?? throw new InvalidOperationException($"Cannot find item {value}.");
        }
    }
}