using System;
using System.Collections.Generic;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='BattleDialog']")]
    public sealed class DialogActionBattleDialog : DialogAction, IMessageHandler
    {
        [XPath("@SpeakerInfo")] public String SpeakerInfo;
        [XPath("@Slot")] public String Slot;
        [XPath("@SpeakerEmotion")] public String SpeakerEmotion;
        [XPath("@Mode")] public String Mode;
        [XPath("@Close")] public Boolean Close;
        [XPath("@Message")] public String? Message;
        [XPath("@DialogType")] public String DialogType;
        [XPath("@Effect")] public String Effect;
        
        [XPath("Message")] public DialogMessage NestedMessage;

        public TextReference MessageId;
        
        public override void Translate(LocalizationTree tree)
        {
            if (!tree.TryGet(nameof(Message), out var child))
                return;
            
            if (Message is null)
                NestedMessage.Translate(child, out MessageId);
            else
                MessageId = child.Value;
        }
        
        public IEnumerable<(String name, TextReference key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(IStage stage)
        {
            var speaker = new StageSpeakerInfo(SpeakerInfo, SpeakerEmotion);
            yield return ("Message", MessageId, speaker);
        }
    }
}