using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Dashboard[@Type='TimeLimiter']")]
    public sealed class StageDashboardTimeLimiter : StageDashboard, IMessageHandler
    {
        [XPath("@Active")] public Boolean Active;
        [XPath("@LimitTime")] public Int64 LimitTime;
        [XPath("@Message")] public String MessageId;
        
        public IEnumerable<(String name, TextReference key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(IStage stage)
        {
            yield return ("Message", TextReference.Sentence(MessageId), null);
        }
    }
}