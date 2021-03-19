using System;

namespace Troublemaker.Xml
{
    [XPath("self::property")]
    public sealed class XmlObjectInfoEmotion
    {
        [XPath("@name")] public String Name;
        [XPath("@Image")] public String Image;
        [XPath("@Icon")] public String Icon;
    }
}