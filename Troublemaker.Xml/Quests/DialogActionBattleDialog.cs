using System;
using System.Collections.Generic;

namespace Troublemaker.Xml.Quests
{
    [XPath("self::property[@Type='BattleDialog']")]
    public sealed class DialogQuestActionBattleDialog : DialogQuestAction, IMessageHandler
    {
        [XPath("@SpeakerInfo")] public String? SpeakerInfo;
        [XPath("@Slot")] public String Slot;
        [XPath("@SpeakerEmotion")] public String SpeakerEmotion;
        [XPath("@Mode")] public String Mode;
        [XPath("@Close")] public Boolean Close;
        [XPath("@Message")] public String? Message;
        [XPath("@DialogType")] public String DialogType;
        [XPath("@Effect")] public String Effect;
        
        public TextReference MessageId;

        public override void Translate(LocalizationTree tree)
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