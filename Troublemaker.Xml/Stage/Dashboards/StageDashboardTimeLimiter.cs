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
        
        public IEnumerable<(String name, String key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(Stage stage)
        {
            yield return ("Message", MessageId, null);
        }
    }
}