using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Dashboard[@Type='EscortCounter']")]
    public sealed class StageDashboardEscortCounter : StageDashboard, IMessageHandler
    {
        [XPath("@Message")] public String MessageId;
        
        public IEnumerable<(String name, String key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(Stage stage)
        {
            yield return ("Message", MessageId, null);
        }
    }
}