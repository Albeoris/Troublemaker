using System;
using System.Linq;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='Dialog']")]
    public sealed class DialogActionDialog : DialogAction
    {
        [XPath("@C_DlgName")] public String DlgName;
        [XPath("property/@Text")] public String[] Lines;
        
        public TextId[] LineIds { get; private set; } = Array.Empty<TextId>();

        public override void Translate(LocalizationTree tree)
        {
            if (!(Lines?.Length > 0))
                return;
            
            LineIds = new TextId[Lines.Length];
            for (Int32 i = 0; i < LineIds.Length; i++)
            {
                if (tree.TryGet(i, out var child))
                    LineIds[i] = child["Text"].Value;
            }
        }
    }
}