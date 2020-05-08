using System;
using System.Collections.Generic;
using Troublemaker.Framework;

namespace Troublemaker.Xml.Quests
{
    [XPath("self::property")]
    public abstract class DialogQuestAction : IExpandable
    {
        public String NodeName => GetType().Name.TrimPrefix(nameof(DialogQuestAction));
        public virtual IEnumerable<(String name, IExpandable expandable)> EnumerateChildren() => Array.Empty<(String name, IExpandable expandable)>();
        public virtual void Translate(LocalizationTree tree) => throw new NotSupportedException(GetType().FullName);
    }
}