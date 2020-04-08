using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property")]
    public sealed class DialogSelection
    {
        [XPath("@Text")] public String Text;
        [XPath("@Notice")] public String Notice;
        [XPath("@Value")] public String Value;

        public TextId TextId { get; private set; }
        public TextId NoticeId { get; private set; }

        public void Translate(LocalizationTree tree)
        {
            if (tree.TryGet(nameof(Text), out var text))
                TextId = text.Value;
            
            if (tree.TryGet(nameof(Notice), out var notice))
                NoticeId = notice.Value;
        }
    }
}