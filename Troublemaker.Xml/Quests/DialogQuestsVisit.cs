using System;
using System.Collections.Generic;

namespace Troublemaker.Xml.Quests
{
    [XPath("self::property[@name]")]
    public sealed class DialogQuestsVisit : IMessageHandler
    {
        [XPath("@name")] public String? Name;
        [XPath("@SpeakerInfo")] public String? SpeakerInfo;
        [XPath("@SpeakerEmotion")] public String SpeakerEmotion;
        [XPath("@Message")] public String Message;

        public TextReference MessageId;

        public String NodeName => Name;
        public IEnumerable<(String name, IExpandable expandable)> EnumerateChildren() => Array.Empty<(String name, IExpandable expandable)>();
        
        public void Translate(LocalizationTree tree)
        {
            if (!tree.TryGet(nameof(Message), out var child))
                return;

            MessageId = child.Value;
        }
        
        public IEnumerable<(String name, TextReference key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(IStage stage)
        {
            var speaker = new StageSpeakerInfo(SpeakerInfo, SpeakerEmotion);
            yield return ("Message", MessageId, speaker);
        }
    }
}