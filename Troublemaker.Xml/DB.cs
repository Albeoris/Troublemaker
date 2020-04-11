using System;

namespace Troublemaker.Xml
{
    public static class DB
    {
        public static DbObjects Objects { get; } = Deserialize<DbObjects>("Objectinfo.xml");
        public static DbMonsters Monsters { get; } = Deserialize<DbMonsters>("Monster.xml");
        public static DbMissions Missions { get; } = Deserialize<DbMissions>("mission.xml");
        public static DbNpc Npc { get; } = Deserialize<DbNpc>("Npc.xml");

        private static T Deserialize<T>(String xmlPath)
        {
            return  XmlDeserializerFactory.Default.Deserialize<T>($"Data/xml/{xmlPath}");
        }
    }
}
