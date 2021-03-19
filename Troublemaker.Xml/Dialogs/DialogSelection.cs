using System;
using System.Collections.Generic;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property")]
    public sealed class DialogSelection : IExpandable, IMessageHandler
    {
        [XPath("@Text")] public String Text;
        [XPath("@Notice")] public String Notice;
        [XPath("@Value")] public String Value;

        public TextReference TextId { get; private set; }
        public TextReference NoticeId { get; private set; }

        public void Translate(LocalizationTree tree)
        {
            if (tree.TryGet(nameof(Text), out var text))
                TextId = text.Value;

            if (tree.TryGet(nameof(Notice), out var notice))
                NoticeId = notice.Value;
        }

        public String NodeName => "Selection";

        public IEnumerable<(String name, IExpandable expandable)> EnumerateChildren() => Array.Empty<(String name, IExpandable expandable)>();

        public IEnumerable<(String name, TextReference key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(IStage stage)
        {
            yield return (nameof(Text), TextId, null);
            yield return (nameof(Notice), NoticeId, null);
        }
    }
}