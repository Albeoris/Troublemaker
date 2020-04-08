using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='SkipPointOff']")]
    public sealed class StageActionSkipPointOff : StageAction
    {
        [XPath("@Name")] public String Name;
        [XPath("@Range")] public Int64 Range;
    }
}