using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='DashboardEvaluator']")]
    public sealed class StageConditionDashboardEvaluator : StageCondition, IMessageHandler
    {
        [XPath("@DashboardKey")] public String DashboardKey;
        [XPath("@FailExpression")] public String FailExpression;
        [XPath("@SuccessExpression")] public String SuccessExpression;
        [XPath("@Key")] public String Key;
        [XPath("@Title")] public String TitleId;

        [XPath("OnSuccessActionList/*")] public StageAction[] OnSuccessActionList;
        
        public override IEnumerable<(String name, IExpandable expandable)> EnumerateChildren()
        {
            yield return OnSuccessActionList.Named(nameof(OnSuccessActionList));
        }
        
        public IEnumerable<(String name, String key, StageSpeakerInfo? speaker)> EnumerateMessageKeys(Stage stage)
        {
            yield return ("Title", TitleId, null);
        }
    }
}