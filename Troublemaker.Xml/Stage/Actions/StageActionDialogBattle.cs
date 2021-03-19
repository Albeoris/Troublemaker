using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='DialogBattle']")]
    public sealed class StageActionDialogBattle : StageAction, IMessageHandler
    {
        [XPath("@CloseDialog")] public Boolean CloseDialog;
        [XPath("@DialogEffect")] public String DialogEffect;
        [XPath("@DialogMode")] public String DialogMode;
        [XPath("@DialogType")] public String DialogType;
        [XPath("@Message")] public String MessageId;
        [XPath("@ShowSlot")] public String ShowSlot;
        [XPath("@Title")] public String TitleId;

        [XPath("Speaker")] public StageSpeaker Speaker;
        
        public IEnumerable<(String name, TextReference key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(IStage stage)
        {
            var speaker = new StageSpeakerInfo(Speaker);
            yield return ("Title", TextReference.Sentence(TitleId), speaker);
            yield return ("Message", TextReference.Sentence(MessageId), speaker);
        }
    }
}