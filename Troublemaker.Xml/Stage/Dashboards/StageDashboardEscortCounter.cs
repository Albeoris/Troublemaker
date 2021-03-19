using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Dashboard[@Type='EscortCounter']")]
    public sealed class StageDashboardEscortCounter : StageDashboard, IMessageHandler
    {
        [XPath("@Message")] public String MessageId;
        
        public IEnumerable<(String name, TextReference key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(IStage stage)
        {
            yield return ("Message", TextReference.Sentence(MessageId), null);
        }
    }
}