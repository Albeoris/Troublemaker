using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='ObjectInteractionEvent']")]
    public sealed class StageConditionObjectInteractionEvent : StageCondition
    {
        [XPath("@Interaction")] public String Interaction;
        [XPath("Unit")] public StagePointObject Unit;
    }
}