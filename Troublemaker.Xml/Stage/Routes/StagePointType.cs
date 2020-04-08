using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='Type']")]
    public sealed class StagePointType : StagePoint
    {
        [XPath("@GameObject")] public String GameObject;
        [XPath("@Team")] public String Team;
    }
}