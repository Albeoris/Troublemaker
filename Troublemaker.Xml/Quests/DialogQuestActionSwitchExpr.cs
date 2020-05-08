using System;
using System.Collections.Generic;

namespace Troublemaker.Xml.Quests
{
    [XPath("self::property[@Type='SwitchExpr']")]
    public sealed class DialogQuestActionSwitchExpr : DialogQuestAction
    {
        [XPath("@MultiAccept")] public Boolean MultiAccept;
        
        [XPath("property")] public DialogQuestActionCase[] Cases { get; set; }
        
        public override void Translate(LocalizationTree tree)
        {
            for (int i = 0; i < Cases.Length; i++)
            {
                if (tree.TryGet(i, out var child))
                    Cases[i].Translate(child);
            }
        }

        public Boolean CanFlatten => false;
        
        public override IEnumerable<(String name, IExpandable expandable)> EnumerateChildren()
        {
            yield return Cases.Named(nameof(Cases));
        }
    }
}