using Troublemaker.Framework;

namespace Troublemaker.Xml
{
    [XPath("self::idspaces")]
    public sealed class DbStatuses
    {
        [XPath("idspace[@id='StatusType']/class/@name")]
        public Map<XmlStatusType> Types;

        [XPath("idspace[@id='Status']/class/@name")]
        public Map<XmlStatus> Entries;
        
        [XPath("idspace[@id='Status']/schema/rule/@property")]
        public Map<XmlRule> EntriesSchema;
    }
}