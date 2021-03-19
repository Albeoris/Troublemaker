using System;

namespace Troublemaker.Xml
{
    [XPath(".")]
    public struct CameraPosition
    {
        [XPath("@Angle")] public Double Angle;
        [XPath("@Px")] public Int64 X;
        [XPath("@Py")] public Int64 Y;
        [XPath("@Pz")] public Int64 Z;
    }
}