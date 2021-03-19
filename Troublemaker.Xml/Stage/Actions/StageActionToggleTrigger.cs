using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='ToggleTrigger']")]
    public sealed class StageActionToggleTrigger : StageAction
    {
        [XPath("@Trigger")] public String Trigger;
    }
}