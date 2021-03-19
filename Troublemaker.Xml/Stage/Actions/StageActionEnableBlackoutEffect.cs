using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='EnableBlackoutEffect']")]
    public sealed class StageActionEnableBlackoutEffect : StageAction
    {
        [XPath("@Name")] public String Name;

        [XPath("Area")] public StageArea Area;
    }
}