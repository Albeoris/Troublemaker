using System;

namespace Troublemaker.Xml
{
    public static class DB
    {
        public static DbObjects Objects { get; } = Deserialize<DbObjects>("Objectinfo.xml");
        public static DbColors Colors { get; } = Deserialize<DbColors>("Color.xml");
        public static DbMasteries Masteries { get; } = Deserialize<DbMasteries>("Mastery.xml");
        public static DbMasterySets MasterySets { get; } = Deserialize<DbMasterySets>("MasterySet.xml");
        public static DbMonsters Monsters { get; } = Deserialize<DbMonsters>("Monster.xml");
        public static DbMissions Missions { get; } = Deserialize<DbMissions>("mission.xml");
        public static DbNpc Npc { get; } = Deserialize<DbNpc>("Npc.xml");
        public static DbStatuses Statuses { get; } = Deserialize<DbStatuses>("Status.xml");

        private static T Deserialize<T>(String xmlPath)
        {
            return  XmlDeserializerFactory.Default.Deserialize<T>($"Data/xml/{xmlPath}");
        }
    }
}