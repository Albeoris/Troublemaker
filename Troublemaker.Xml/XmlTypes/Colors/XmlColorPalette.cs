using System;
using Troublemaker.Framework;

namespace Troublemaker.Xml
{
    [XPath("self::class")]
    public sealed class XmlColorPalette : IXmlObject
    {
        //<class name="SequentialBuGn5" Palette="edf8fb, b2e2e2, 66c2a4, 2ca25f, 006d2c"/>
        [XPath("@name")] public String Name; // "SequentialBuGn5"
        [XPath("@Palette")] public String Palette; // "edf8fb, b2e2e2, 66c2a4, 2ca25f, 006d2c""

        [XPath("@*")] public Map<String> Attributes { get; set; }
    }
}