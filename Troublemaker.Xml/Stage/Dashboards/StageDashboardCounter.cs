using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Dashboard[@Type='Counter']")]
    public sealed class StageDashboardCounter : StageDashboard, IMessageHandler
    {
        [XPath("@Linked")] public Boolean Linked;
        [XPath("@Message")] public String MessageId;
        [XPath("@Variable")] public String Variable;
        
        public IEnumerable<(String name, TextReference key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(IStage stage)
        {
            yield return ("Message", TextReference.Sentence(MessageId), null);
        }
    }
}