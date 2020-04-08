using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='SkipPointOn']")]
    public sealed class StageActionSkipPointOn : StageAction
    {
        [XPath("@Name")] public String Name;
        [XPath("@Range")] public Int64 Range;
    }
}