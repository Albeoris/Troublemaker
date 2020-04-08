using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='BattleDialog']")]
    public sealed class DialogActionBattleDialog : DialogAction
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

        public TextId MessageId;
        
        public override void Translate(LocalizationTree tree)
        {
            if (tree.TryGet(nameof(Message), out var child))
            {
                if (Message is null)
                {
                    NestedMessage.Translate(child, out MessageId);
                }
                else
                {
                    MessageId = child.Value;
                }
            }
        }
    }
}