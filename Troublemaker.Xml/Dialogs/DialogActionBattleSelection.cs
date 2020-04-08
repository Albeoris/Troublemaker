using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='BattleSelection']")]
    public sealed class DialogActionBattleSelection : DialogAction
    {
        [XPath("@SpeakerInfo")] public String SpeakerInfo;
        [XPath("@SpeakerEmotion")] public String SpeakerEmotion;
        [XPath("@Mode")] public String Mode;
        [XPath("@Close")] public Boolean Close;
        [XPath("@Message")] public String Message;
        [XPath("@DialogType")] public String DialogType;
        [XPath("@Slot")] public String Slot;
        [XPath("@Effect")] public String Effect;
        [XPath("@Target")] public String Target;
        
        [XPath("property/@Text")] public String[] Lines;

        public TextId MessageId { get; private set; }
        public TextId[] LineIds { get; private set; } = Array.Empty<TextId>();

        public override void Translate(LocalizationTree tree)
        {
            if (tree.TryGet(nameof(Message), out var message))
                MessageId = message.Value;

            if (!(Lines?.Length > 0))
                return;
            
            LineIds = new TextId[Lines.Length];
            for (Int32 i = 0; i < LineIds.Length; i++)
            {
                if (tree.TryGet(i, out var child))
                    LineIds[i] = child["Text"].Value;
            }
        }
    }
}