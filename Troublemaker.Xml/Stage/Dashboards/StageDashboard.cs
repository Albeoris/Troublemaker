using System;
using System.Collections.Generic;
using Troublemaker.Framework;

namespace Troublemaker.Xml
{
    [XPath("self::Dashboard")]
    public abstract class StageDashboard : IExpandable
    {
        [XPath("@Key")] public String Key;
        [XPath("@StageKey")] public String StageKey;
        [XPath("@Show")] public Boolean Show;

        public virtual String NodeName => $"{GetType().Name.TrimPrefix("StageDashboard")} ({Key})";
        public virtual IEnumerable<(String name, IExpandable expandable)> EnumerateChildren() => Array.Empty<(String name, IExpandable expandable)>();
    }
}