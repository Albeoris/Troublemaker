using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='ToggleTriggerGroup']")]
    public sealed class StageActionToggleTriggerGroup : StageAction
    {
        [XPath("@TriggerGroup")] public String TriggerGroup;
    }
}