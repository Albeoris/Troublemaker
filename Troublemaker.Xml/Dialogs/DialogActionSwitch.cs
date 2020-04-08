using System;
using Troublemaker.Framework;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='Switch']")]
    public sealed class DialogActionSwitch : DialogAction
    {
        [XPath("@C_TestTarget")] public String TestTarget;
        
        [XPath("property")] public DialogActionSwitchCase[] Cases { get; set; }
        
        public override void Translate(LocalizationTree tree)
        {
            for (int i = 0; i < Cases.Length; i++)
            {
                if (tree.TryGet(i, out var child))
                    Cases[i].Translate(child);
            }
        }
    }
}