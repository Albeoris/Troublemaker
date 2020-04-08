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
        
        public IEnumerable<(String name, String key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(Stage stage)
        {
            var speaker = new StageSpeakerInfo(Speaker);
            yield return ("Title", TitleId, speaker);
            yield return ("Message", MessageId, speaker);
        }
    }
}