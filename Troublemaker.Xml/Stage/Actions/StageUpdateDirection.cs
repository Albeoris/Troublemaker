using System;

namespace Troublemaker.Xml
{
    [XPath("self::UpdateDirection")]
    public sealed class StageUpdateDirection
    {
        [XPath("@Type")] public String Type;
        [XPath("@SystemDirection")] public Int64 SystemDirection;
    }
}