using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='OfficeContract']")]
    public sealed class DialogActionOfficeContract : DialogAction
    {
        [XPath("@Title")] public String Title;
        [XPath("@C_OfficeName")] public String OfficeName;
        [XPath("@Target")] public String Target;

        public TextId TitleId { get; private set; }

        public override void Translate(LocalizationTree tree)
        {
            if (tree.TryGet(nameof(Title), out var title))
                TitleId = title.Value;
        }
    }
}