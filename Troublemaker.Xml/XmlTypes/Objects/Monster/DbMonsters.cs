using System;
using System.Collections.Generic;
using Troublemaker.Framework;

namespace Troublemaker.Xml
{
    [XPath("self::idspace[@id='Monster']")]
    public sealed class DbMonsters
    {
        [XPath("class/@name")] public Map<XmlMonster> Items;

        public XmlMonster this[String name] => Items[name];
    }
}