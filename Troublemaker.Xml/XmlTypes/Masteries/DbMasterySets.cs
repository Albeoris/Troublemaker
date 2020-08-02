using Troublemaker.Framework;

namespace Troublemaker.Xml
{
    [XPath("self::idspaces")]
    public sealed class DbMasterySets
    {
        [XPath("idspace[@id='MasterySet']/class/@name")]
        public Map<XmlMasterySet> Entries;
    }
}