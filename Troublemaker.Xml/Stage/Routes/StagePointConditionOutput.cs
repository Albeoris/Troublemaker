using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='ConditionOutput']")]
    public sealed class StagePointConditionOutput : StagePoint
    {
        [XPath("@Key")] public String Key;
    }
}