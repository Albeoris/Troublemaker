using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='SelDialogBattle']")]
    public sealed class StageActionSelDialogBattle : StageAction, IMessageHandler
    {
        [XPath("@CloseDialog")] public Boolean CloseDialog;
        [XPath("@DBKey")] public String DBKey;
        [XPath("@DialogEffect")] public String DialogEffect;
        [XPath("@DialogMode")] public String DialogMode;
        [XPath("@DialogType")] public String DialogType;
        [XPath("@Message")] public String MessageId;
        [XPath("@ShowSlot")] public String ShowSlot;

        [XPath("DialogChoice/*")] public StageChoice[] DialogChoice;
        [XPath("Speaker")] public StageSpeaker Speaker;
        
        public override IEnumerable<(String name, IExpandable expandable)> EnumerateChildren()
        {
            yield return DialogChoice.Named(nameof(DialogChoice));
        }

        public IEnumerable<(String name, String key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(Stage stage)
        {
            var speaker = new StageSpeakerInfo(Speaker);
            yield return (nameof(MessageId), MessageId, speaker);
        }
    }
}