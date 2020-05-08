using System;
using System.Collections.Generic;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='Loop']")]
    public sealed class DialogActionLoop : DialogAction, IExpandable
    {
        [XPath("@LoopCount")] public Int32 LoopCount;

        [XPath("property")] public DialogAction[] Actions { get; set; }

        public override void Translate(LocalizationTree tree, DialogScript script, Dialog dialog)
        {
            for (int i = 0; i < Actions.Length; i++)
            {
                if (tree.TryGet(i, out var child))
                    Actions[i].Translate(child, script, dialog);
            }
        }

        public override String NodeName => $"Loop: {LoopCount}";

        public override IEnumerable<(String name, IExpandable expandable)> EnumerateChildren()
        {
            yield return Actions.Named(NodeName);
        }
    }
}