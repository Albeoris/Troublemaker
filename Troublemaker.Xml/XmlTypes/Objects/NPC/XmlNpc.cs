using System;

namespace Troublemaker.Xml
{
    [XPath("self::class")]
    public sealed class XmlNpc
    {
        [XPath("@name")] public String Name;
        [XPath("@Info")] public String Info;
        [XPath("@Type")] public String Type;
        [XPath("@Object")] public String Object;
        [XPath("@Mark")] public String Mark;
        [XPath("@MarkImage")] public String MarkImage;
        [XPath("@Dialog")] public String Dialog;
        [XPath("@IsMinimap")] public String IsMinimap;
    }
}