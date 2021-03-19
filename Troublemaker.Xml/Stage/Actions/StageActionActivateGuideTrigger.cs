using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='ActivateGuideTrigger']")]
    public sealed class StageActionActivateGuideTrigger : StageAction
    {
        [XPath("@GuideTrigger")] public String GuideTrigger;
    }
}