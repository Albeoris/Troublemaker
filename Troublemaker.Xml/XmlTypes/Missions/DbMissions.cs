using System;
using Troublemaker.Framework;

namespace Troublemaker.Xml
{
    [XPath("self::idspaces")]
    public sealed class DbMissions
    {
        [XPath("idspace[@id='Mission']/class/@name")]
        public Map<XmlMission> Missions;
    }
}