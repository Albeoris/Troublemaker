using System;
using System.Collections.Generic;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='TitleMessage']")]
    public sealed class DialogActionTitleMessage : DialogAction, IMessageHandler
    {
        [XPath("@Title")] public String Title;
        [XPath("@Message")] public String Message;
        
        public TextReference TitleId { get; private set; }
        public TextReference MessageId { get; private set; }

        public override void Translate(LocalizationTree tree, DialogScript dialogScript, Dialog dialog)
        {
            if (tree.TryGet(nameof(Message), out var message))
                MessageId = message.Value;

            if (tree.TryGet(nameof(Title), out var title))
                TitleId = title.Value;
        }

        public IEnumerable<(String name, TextReference key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(IStage stage)
        {
            yield return (nameof(Title), TitleId, null);
            yield return (nameof(Message), MessageId, null);
        }
    }
}