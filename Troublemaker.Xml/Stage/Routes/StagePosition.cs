using System;

namespace Troublemaker.Xml
{
    [XPath(".")]
    public struct StagePosition
    {
        [XPath("@x")] public Double X;
        [XPath("@y")] public Double Y;
        [XPath("@z")] public Double Z;
    }
}