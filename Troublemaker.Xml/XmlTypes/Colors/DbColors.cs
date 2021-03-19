using Troublemaker.Framework;

namespace Troublemaker.Xml
{
    [XPath("self::idspaces")]
    public sealed class DbColors
    {
        [XPath("idspace[@id='ColorPalette']/class/@name")]
        public Map<XmlColorPalette> Palettes;

        [XPath("idspace[@id='Color']/class/@name")]
        public Map<XmlColor> Entries;
        
        [XPath("idspace[@id='Color']/schema/rule/@property")]
        public Map<XmlRule> EntriesSchema;
    }
}