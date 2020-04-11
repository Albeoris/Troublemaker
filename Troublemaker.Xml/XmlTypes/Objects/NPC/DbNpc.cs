using System;
using System.Linq;
using Troublemaker.Framework;

namespace Troublemaker.Xml
{
    [XPath("self::idspace[@id='Npc']")]
    public sealed class DbNpc
    {
        [XPath("class/@name")] public Map<XmlNpc> Items;

        public XmlNpc this[String name] => Items[name];

        public XmlNpc? FindByDialogName(String? name)
        {
            if (String.IsNullOrEmpty(name))
                return null;

            return Items.FirstOrDefault<XmlNpc>(n => n.Dialog == name);
        }
    }
}