using System;

namespace Troublemaker.Xml
{
    [XPath("self::image")]
    public sealed class ImageSetBaseImage
    {
        [XPath("@name")] public String Name;
        [XPath("@file")] public String File;
        [XPath("@height")] public Int32 Height;
        [XPath("@width")] public Int32 Width;
        [XPath("@xPos")] public Int32 X;
        [XPath("@yPos")] public Int32 Y;
    }
}