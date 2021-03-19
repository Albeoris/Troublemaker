using System;
using System.Collections.Generic;
using System.Text;
using Troublemaker.Framework;

namespace Troublemaker.Xml
{
    [XPath("self::idspace[@id='ObjectInfo']")]
    public sealed class DbObjects
    {
        [XPath("class/@name")] public Map<XmlObject> Items;
        public XmlObject this[String name] => Items[name];
    }
}
