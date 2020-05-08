using System;
using System.Collections.Generic;

namespace Troublemaker.Xml.Quests
{
    [XPath("self::property[@Case]")]
    public sealed class DialogQuestActionCase : IExpandable
    {
        [XPath("@Case")] public String? Case;
        
        [XPath("property")] public DialogQuestAction[] Actions { get; set; }

        public void Translate(LocalizationTree tree)
        {
            for (int i = 0; i < Actions.Length; i++)
            {
                if (tree.TryGet(i, out var child))
                    Actions[i].Translate(child);
            }
        }

        public String NodeName => $"Case: {Case}";
        public Boolean CanFlatten => false;

        public IEnumerable<(String name, IExpandable expandable)> EnumerateChildren()
        {
            yield return Actions.Named(NodeName);
        }
    }
}