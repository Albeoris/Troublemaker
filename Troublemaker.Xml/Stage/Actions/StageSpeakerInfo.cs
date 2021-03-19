using System;

namespace Troublemaker.Xml
{
    public sealed class StageSpeakerInfo
    {
        public StageSpeakerInfo(StageSpeaker speaker)
            : this(speaker?.Info, speaker?.Emotion)
        {
            ImagePath = speaker?.ImagePath;
        }

        public StageSpeakerInfo(String name, String emotion)
        {
            Name = name;
            Emotion = emotion;
        }

        public String Name { get; set; }
        public String Emotion { get; set; }
        public StageActionBalloonType? Floating { get; set; }
        public String ImagePath { get; set; }
    }
}