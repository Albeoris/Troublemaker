using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Dashboard[@Type='HitList']")]
    public sealed class StageDashboardHitList : StageDashboard, IMessageHandler
    {
        [XPath("@Message")] public String MessageId;
        
        [XPath("ObjectKeyList/ObjectKey/@ObjectKey")] public String[] ObjectKeyList;
        
        public IEnumerable<(String name, TextReference key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(IStage stage)
        {
            yield return ("Message", TextReference.Sentence(MessageId), null);
        }
    }
}