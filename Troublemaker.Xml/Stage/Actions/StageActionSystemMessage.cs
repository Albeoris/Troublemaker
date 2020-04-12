using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='SystemMessage']")]
    public sealed class StageActionSystemMessage : StageAction, IMessageHandler
    {
        [XPath("@Message")] public String MessageId;
        [XPath("@Title")] public String TitleId;
        
        public IEnumerable<(String name, TextReference key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(IStage stage)
        {
            yield return ("Title", TextReference.Sentence(TitleId), null);
            yield return ("Message", TextReference.Sentence(MessageId), null);
        }
    }
}