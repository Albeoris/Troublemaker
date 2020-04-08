using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='SceneFade']")]
    public sealed class StageActionSceneFade : StageAction
    {
        [XPath("@Direct")] public Boolean Direct;
        [XPath("@FadeType")] public String FadeType;
        [XPath("@Image")] public String Image;
    }
}