using System;
using System.Collections.Generic;
using Troublemaker.Framework;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::class")]
    public sealed class DialogScript : IExpandable
    {
        [XPath("@name")] public String Name { get; set; }

        [XPath("Scripts/property")] public DialogAction[] Actions { get; set; }

        public void Translate(LocalizationTree tree)
        {
            tree = tree["Scripts"];
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