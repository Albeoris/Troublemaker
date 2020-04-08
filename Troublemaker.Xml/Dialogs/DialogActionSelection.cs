using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='Selection']")]
    public sealed class DialogActionSelection : DialogAction
    {
        [XPath("@C_DlgName")] public String DlgName;
        [XPath("@Content")] public String Content;
        [XPath("@C_Price")] public String Price;
        [XPath("@Target")] public String Target;
        
        [XPath("property")] public DialogSelection[] Selection;

        public TextId ContentId { get; private set; }

        public override void Translate(LocalizationTree tree)
        {
            if (tree.TryGet(nameof(Content), out var content))
                ContentId = content.Value;

            if (!(Selection?.Length > 0))
                return;
            
            for (Int32 i = 0; i < Selection.Length; i++)
            {
                if (tree.TryGet(i, out var child))
                    Selection[i].Translate(child);
            }
        }
    }
}