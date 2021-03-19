using System;
using System.Collections.Generic;
using Troublemaker.Framework;

namespace Troublemaker.Xml
{
    [XPath(".")]
    public abstract class StageMapComponent : IExpandable
    {
        [XPath("@Key")] public String Key { get; set; }
        [XPath("@Group")] public String Group;
        
        public virtual String NodeName => $"{GetType().Name.TrimPrefix("StageMapComponent")} ({Key})";
        public virtual IEnumerable<(String name, IExpandable expandable)> EnumerateChildren() => Array.Empty<(String name, IExpandable expandable)>();
    }
}