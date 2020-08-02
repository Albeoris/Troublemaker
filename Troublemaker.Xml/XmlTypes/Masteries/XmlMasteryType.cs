using System;
using Troublemaker.Framework;

namespace Troublemaker.Xml
{
    [XPath("self::class")]
    public sealed class XmlMasteryType : IXmlObject
    {
        [XPath("@name")] public String Name;                       // "Lightning"
        [XPath("@Title")] public String Title;                     // "번개"
        [XPath("@Order")] public Int64 Order;                      // "22"
        [XPath("@CheckType")] public String CheckType;             // "ESP"
        [XPath("@Image")] public String Image;                     // "Mastery/Lightning"
        [XPath("@Image_Disabled")] public String Image_Disabled;   // "Mastery_Disabled/Lightning"
        
        [XPath("@*")] public Map<String> Attributes { get; set; }
    }
}