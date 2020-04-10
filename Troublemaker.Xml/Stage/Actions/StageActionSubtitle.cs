using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='Subtitle']")]
    public sealed class StageActionSubtitle : StageAction, IMessageHandler
    {
        [XPath("@BalloonType")] public String BalloonType;
        [XPath("@DialogEffect")] public String DialogEffect;
        [XPath("@DialogMode")] public String DialogMode;
        [XPath("@DialogType")] public String DialogType;
        [XPath("@Font")] public String Font;
        [XPath("@FontColor")] public String FontColor;
        [XPath("@LifeTime")] public Double LifeTime;
        [XPath("@Message")] public String MessageId;
        [XPath("@ShowSlot")] public String ShowSlot;
        [XPath("@Time")] public Double Time;

        [XPath("Speaker")] public StageSpeaker Speaker;
        [XPath("Unit")] public StagePointObject Unit;
        
        public IEnumerable<(String name, TextReference key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(IStage stage)
        {
            StageSpeakerInfo speaker = new StageSpeakerInfo(Speaker) {Floating = StageActionBalloonType.Wrap(BalloonType)};
            yield return ("Message", TextReference.Sentence(MessageId), speaker);
        }
    }
}