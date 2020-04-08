using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='BalloonChat']")]
    public sealed class StageActionBalloonChat : StageAction, IMessageHandler
    {
        [XPath("@BalloonType")] public String BalloonType;
        [XPath("@Font")] public String Font;
        [XPath("@LifeTime")] public Double LifeTime;
        [XPath("@Message")] public String MessageId;
        [XPath("@Time")] public Double Time;

        [XPath("Unit")] public StagePoint Unit;
        
        public IEnumerable<(String name, String key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(Stage stage)
        {
            StageActionBalloonType balloonType = StageActionBalloonType.Wrap(BalloonType);
            StageSpeakerObject speakerObj = new StageSpeakerObject(Unit);
            StageSpeaker speaker = speakerObj.Resolve(stage);
            StageSpeakerInfo speakerInfo = new StageSpeakerInfo(speaker) {Floating = balloonType};
            yield return ("Message", MessageId, speakerInfo);
        }
    }
}