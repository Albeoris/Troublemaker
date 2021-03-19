using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Entry")]
    public sealed class StageGoalCondition : IExpandable, IMessageHandler
    {
        [XPath("@FontColor")] public String FontColor;
        [XPath("@Title")] public String TitleId;

        public String NodeName => $"GoalCondition";
        public IEnumerable<(String name, IExpandable expandable)> EnumerateChildren() => Array.Empty<(String name, IExpandable expandable)>(); 
        
        public IEnumerable<(String name, TextReference key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(IStage stage)
        {
            yield return ("Title", TextReference.Sentence(TitleId), null);
        }
    }
}