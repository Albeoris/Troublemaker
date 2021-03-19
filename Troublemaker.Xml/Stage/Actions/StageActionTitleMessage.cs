using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='TitleMessage']")]
    public sealed class StageActionTitleMessage : StageAction, IMessageHandler
    {
        [XPath("@Image")] public String Image;
        [XPath("@Title")] public String TitleId;
        [XPath("@Message")] public String MessageId;
        
        public IEnumerable<(String name, TextReference key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(IStage stage)
        {
            yield return ("Title", TextReference.Sentence(TitleId), null);
            yield return ("Message", TextReference.Sentence(MessageId), null);
        }
    }
}