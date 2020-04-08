using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='TriggerCondition']")]
    public sealed class StageConditionTriggerCondition : StageCondition, IMessageHandler
    {
        [XPath("@Key")] public String Key;
        [XPath("@LoseOnFail")] public Boolean LoseOnFail;
        [XPath("@Title")] public String TitleId;

        [XPath("FailCondition")] public StageCondition FailCondition;
        [XPath("SuccessCondition")] public StageCondition SuccessCondition;
        
        public override IEnumerable<(String name, IExpandable expandable)> EnumerateChildren()
        {
            yield return FailCondition.Named(nameof(FailCondition));
            yield return SuccessCondition.Named(nameof(SuccessCondition));
        }
        
        public IEnumerable<(String name, String key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(Stage stage)
        {
            yield return ("Title", TitleId, null);
        }
    }
}