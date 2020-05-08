using System;
using System.Collections.Generic;
using Troublemaker.Xml.Dialogs;

namespace Troublemaker.Xml.Quests
{
    [XPath("self::property")]
    public sealed class DialogQuestScript : IExpandable
    {
        [XPath("@name")] public String Name { get; set; }

        [XPath("Scripts/property")] public DialogQuestAction[] Actions { get; set; }

        public void Translate(LocalizationTree tree)
        {
            if (!tree.TryGet("Scripts", out tree))
                return;
            
            for (int i = 0; i < Actions.Length; i++)
            {
                if (tree.TryGet(i, out var child))
                    Actions[i].Translate(child);
            }
        }

        public String NodeName => Name;
        public IEnumerable<(String name, IExpandable expandable)> EnumerateChildren()
        {
            yield return Actions.Named(nameof(Actions));
        }
    }
}