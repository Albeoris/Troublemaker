using System;
using System.Collections.Generic;
using Troublemaker.Framework;

namespace Troublemaker.Xml
{
    [XPath("self::Action")]
    public abstract class StageAction : IExpandable
    {
        [XPath("@ActionKey")] public String ActionKey;
        [XPath("@Connect")] public String Connect;
        [XPath("@ConnectFrame")] public Int64 ConnectFrame;
        [XPath("@Sequential")] public Boolean Sequential;
        [XPath("@NoEvent")] public String NoEvent;
        [XPath("@NoWait")] public String NoWait;
        [XPath("@OnOff")] public String OnOff;
        [XPath("@OffOn")] public String OffOn;
        
        public virtual String NodeName => $"{GetType().Name.TrimPrefix("StageAction")}";
        public virtual IEnumerable<(String name, IExpandable expandable)> EnumerateChildren() => Array.Empty<(String name, IExpandable expandable)>();
    }
}