using System;
using System.Collections.Generic;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='BattleSelection']")]
    public sealed class DialogActionBattleSelection : DialogAction, IMessageHandler
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
        
        [XPath("property/@Text")] public String[] Text;

        public TextReference MessageId { get; private set; }
        public TextReference[] TextIds { get; private set; } = Array.Empty<TextReference>();

        public override void Translate(LocalizationTree tree)
        {
            if (tree.TryGet(nameof(Message), out var message))
                MessageId = message.Value;

            if (!(Text?.Length > 0))
                return;
            
            TextIds = new TextReference[Text.Length];
            for (Int32 i = 0; i < TextIds.Length; i++)
            {
                if (tree.TryGet(i, out var child))
                    TextIds[i] = child["Text"].Value;
            }
        }
        
        public IEnumerable<(String name, TextReference key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(IStage stage)
        {
            var speaker = new StageSpeakerInfo(SpeakerInfo, SpeakerEmotion);
            yield return ("Message", MessageId, speaker);
            for (var index = 0; index < TextIds.Length; index++)
            {
                TextReference line = TextIds[index];
                yield return ($"Text {index}", line, speaker);
            }
        }
    }
}