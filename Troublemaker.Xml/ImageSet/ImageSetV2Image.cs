using System;

namespace Troublemaker.Xml
{
    [XPath("self::Image")]
    public sealed class ImageSetV2Image
    {
        [XPath("@name")] public String Name;
        [XPath("@height")] public Int32 Height;
        [XPath("@width")] public Int32 Width;
        [XPath("@xPos")] public Int32 X;
        [XPath("@yPos")] public Int32 Y;
    }
}