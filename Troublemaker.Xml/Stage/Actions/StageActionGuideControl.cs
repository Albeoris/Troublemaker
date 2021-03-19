using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='GuideControl']")]
    public sealed class StageActionGuideControl : StageAction
    {
        [XPath("@GuideType")] public String GuideType;
    }
}