using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::MissionDirect")]
    public sealed class StageMissionDirect : IExpandable
    {
        [XPath("@Key")] public String Key;

        [XPath("Action")] public StageAction[] Actions;

        public String NodeName => String.IsNullOrEmpty(Key) ? "<NoName>" : Key;

        public IEnumerable<(String name, IExpandable expandable)> EnumerateChildren()
        {
            yield return Actions.Named(nameof(Actions));
        }
    }
}