using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[not(@Type)]")]
    public sealed class DialogActionNative : DialogAction
    {
        [XPath("@C_DlgName")] public String DialogType;
        [XPath("property/@Text")] public String[] FormatLines;

        public TextId[] FormatLineIds { get; private set; } = Array.Empty<TextId>();

        public override void Translate(LocalizationTree tree)
        {
            if (!(FormatLines?.Length > 0))
                return;
            
            FormatLineIds = new TextId[FormatLines.Length];
            for (int i = 0; i < FormatLineIds.Length; i++)
            {
                if (tree.TryGet(i, out var child))
                    FormatLineIds[i] = child["Text"].Value;
            }
        }
    }
}