using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Case|@C_Case]")]
    public sealed class DialogActionSwitchCase
    {
        [XPath("@Case")] public String Case;
        [XPath("@C_Case")] public String CCase;
        
        [XPath("property")] public DialogAction[] Actions { get; set; }

        public void Translate(LocalizationTree tree)
        {
            for (int i = 0; i < Actions.Length; i++)
            {
                if (tree.TryGet(i, out var child))
                    Actions[i].Translate(child);
            }
        }
    }
}