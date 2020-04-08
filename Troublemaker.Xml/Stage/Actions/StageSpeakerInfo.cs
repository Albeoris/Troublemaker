using System;

namespace Troublemaker.Xml
{
    public sealed class StageSpeakerInfo
    {
        public StageSpeakerInfo(StageSpeaker speaker)
        {
            Name = speaker?.Info;
            Emotion = speaker?.Emotion;
        }

        public String Name { get; set; }
        public String Emotion { get; set; }
        public StageActionBalloonType? Floating { get; set; }
    }
}