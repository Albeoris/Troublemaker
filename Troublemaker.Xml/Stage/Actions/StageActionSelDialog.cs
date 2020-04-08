using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='SelDialog']")]
    public sealed class StageActionSelDialog : StageAction, IMessageHandler
    {
        [XPath("@CloseDialog")] public Boolean CloseDialog;
        [XPath("@DBKey")] public String DBKey;
        [XPath("@Message")] public String MessageId;

        [XPath("DialogChoice/*")] public StageChoice[] DialogChoice;
        
        public override IEnumerable<(String name, IExpandable expandable)> EnumerateChildren()
        {
            yield return DialogChoice.Named(nameof(DialogChoice));
        }
        
        public IEnumerable<(String name, String key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(Stage stage)
        {
            yield return ("Message", MessageId, null);
        }
    }
}