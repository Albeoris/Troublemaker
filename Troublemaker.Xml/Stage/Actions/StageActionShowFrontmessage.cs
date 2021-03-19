using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='ShowFrontmessage']")]
    public sealed class StageActionShowFrontMessage : StageAction, IMessageHandler
    {
        [XPath("@MessageColor")] public String MessageColor;
        [XPath("@Message")] public String MessageId;
        
        public IEnumerable<(String name, TextReference key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(IStage stage)
        {
            yield return ("Message", TextReference.Sentence(MessageId), null);
        }
    }
}