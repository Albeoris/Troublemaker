using System;

namespace Troublemaker.Xml
{
    [XPath("self::Relation")]
    public sealed class StageRelation
    {
        [XPath("@Team1")] public String Team1;
        [XPath("@Team2")] public String Team2;
        [XPath("@Type")] public String Type;
    }
}