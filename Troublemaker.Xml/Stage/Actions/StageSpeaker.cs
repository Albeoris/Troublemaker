using System;
using System.Runtime.CompilerServices;

namespace Troublemaker.Xml
{
    [XPath("self::Speaker")]
    public sealed class StageSpeaker : IStageSpeaker
    {
        [XPath("@Info")] public String Info;
        [XPath("@Emotion")] public String Emotion;
        
        public String ImagePath { get; set; }

        public StageSpeaker Resolve(IStage stage)
        {
            return this;
        }
    }
}