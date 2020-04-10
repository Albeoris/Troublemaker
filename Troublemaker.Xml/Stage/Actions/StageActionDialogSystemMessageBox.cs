using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='DialogSystemMessageBox']")]
    public sealed class StageActionDialogSystemMessageBox : StageAction, IMessageHandler
    {
        [XPath("@Image")] public String Image;
        [XPath("@Reference1")] public String Reference1;
        [XPath("@Reference2")] public String Reference2;
        [XPath("@Reference3")] public String Reference3;
        [XPath("@Reference4")] public String Reference4;

        [XPath("@Message")] public String MessageId;
        [XPath("@Title")] public String TitleId;
        
        public IEnumerable<(String name, TextReference key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(IStage stage)
        {
            yield return ("Title", TextReference.Sentence(TitleId), null);
            yield return ("Message", TextReference.Sentence(MessageId), null);
        }
    }
}