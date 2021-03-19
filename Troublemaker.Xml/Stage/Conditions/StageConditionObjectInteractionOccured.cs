using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='ObjectInteractionOccured']")]
    public sealed class StageConditionObjectInteractionOccured : StageCondition
    {
        [XPath("@Interaction")] public String Interaction;
        [XPath("Unit")] public StagePointObject Unit;
    }
}