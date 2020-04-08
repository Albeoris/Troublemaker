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
        
        public IEnumerable<(String name, String key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(Stage stage)
        {
            yield return ("Title", TitleId, null);
        }
    }
}