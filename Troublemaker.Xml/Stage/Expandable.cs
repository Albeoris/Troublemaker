﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Troublemaker.Xml
{
    public static class Expandable
    {
        public static (String name, IExpandable expandable) Named(this IExpandable expandable, String name)
        {
            return (name, expandable);
        }
        
        public static (String name, IExpandable expandable) Named(this IEnumerable<IExpandable> expandable, String name)
        {
            return (name, new ExpandableCollection(name, expandable));
        }

        public static IEnumerable<(Int32 level, IExpandable?)> EnumerateChildrenRecursively(this IExpandable root, Boolean includeRoot = false)
        {
            if (root is null)
                yield break;
            
            if (includeRoot)
                yield return (0, root);
            
            var queue = new LinkedList<(Int32 level, IExpandable expandable)>();
            Boolean hasValues = false;
            
            foreach ((_, IExpandable child) in root.EnumerateChildren())
            {
                if (child is null)
                    continue;
                
                queue.AddLast((1, child));
                hasValues = true;
            }

            if (hasValues)
                queue.AddLast((1, ExpandableEnd.Instance));

            LinkedListNode<(Int32 level, IExpandable? expandable)>? node;
            while ((node = queue.First) != null)
            {
                (Int32 level, IExpandable? expandable) action = node.Value;
                yield return action;

                if (action.expandable != ExpandableEnd.Instance)
                {
                    var list = action.expandable.EnumerateChildren();

                    hasValues = false;
                    
                    foreach (var (_, expandable) in list)
                    {
                        if (expandable is null)
                            continue;
                        
                        node = queue.AddAfter(node, (action.level + 1, expandable));
                        hasValues = true;
                    }

                    if (hasValues)
                        queue.AddAfter(node, (action.level + 1, ExpandableEnd.Instance));
                }

                queue.RemoveFirst();
            }
        }
    }
}