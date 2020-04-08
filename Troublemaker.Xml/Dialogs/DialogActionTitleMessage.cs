using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='TitleMessage']")]
    public sealed class DialogActionTitleMessage : DialogAction
    {
        [XPath("@Message")] public String Message;
        [XPath("@Title")] public String Title;
        
        public TextId MessageId { get; private set; }
        public TextId TitleId { get; private set; }

        public override void Translate(LocalizationTree tree)
        {
            if (tree.TryGet(nameof(Message), out var message))
                MessageId = message.Value;

            if (tree.TryGet(nameof(Title), out var title))
                TitleId = title.Value;
        }
    }
}