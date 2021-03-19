using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Dashboard[@Type='ObjectiveMarker']")]
    public sealed class StageDashboardObjectiveMarker : StageDashboard, IMessageHandler
    {
        [XPath("@CustomImage")] public String CustomImage;
        [XPath("@Message")] public String MessageId;
        [XPath("@NamedAssetKey")] public String NamedAssetKey;
        [XPath("@YOffset")] public Int64 YOffset;

        [XPath("PositionIndicator")] public StagePoint PositionIndicator;
        
        [XPath("Unit")] public StagePointObject Unit;
        
        public IEnumerable<(String name, TextReference key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(IStage stage)
        {
            yield return ("Message", TextReference.Sentence(MessageId), null);
        }
    }
}