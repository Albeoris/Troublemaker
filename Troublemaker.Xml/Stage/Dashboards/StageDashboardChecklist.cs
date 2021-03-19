using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Dashboard[@Type='Checklist']")]
    public sealed class StageDashboardChecklist : StageDashboard, IMessageHandler
    {
        [XPath("@Message")] public String MessageId;
        [XPath("@Order")] public Int64 Order;
        [XPath("@Turn")] public Int64 Turn;
        
        [XPath("Area")] public StageArea Area;
        [XPath("ExitPos")] public StagePosition ExitPos;
        [XPath("Unit")] public StagePointObject Unit;
        
        public IEnumerable<(String name, TextReference key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(IStage stage)
        {
            yield return ("Message", TextReference.Sentence(MessageId), null);
        }
    }
}