using System;
using Troublemaker.Framework;

namespace Troublemaker.Xml
{
    [XPath("self::class")]
    public sealed class XmlColor : IXmlObject
    {
        //<class  ColorRectGradationAlpha="tl:FFFFC100 tr:FFFFC100 bl:FFFFC100 br:FFFFC100"/>
        [XPath("@name")] public String Name; // "Amber"
        [XPath("@ARGB")] public String ARGB; // "FFFFC100"
        [XPath("@ColorRect")] public String ColorRect; // "l:FFFFC100 tr:FFFFC100 bl:FFFFC100 br:FFFFC100"
        [XPath("@ColorRectGradationAlpha")] public String ColorRectGradationAlpha; // "tl:FFFFC100 tr:FFFFC100 bl:FFFFC100 br:FFFFC100""

        [XPath("@*")] public Map<String> Attributes { get; set; }
    }
}