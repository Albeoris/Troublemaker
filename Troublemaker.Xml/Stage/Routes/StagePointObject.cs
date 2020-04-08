using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='Object']")]
    public sealed class StagePointObject : StagePoint
    {
        [XPath("@ObjectKey")] public String ObjectKey;
    }
}