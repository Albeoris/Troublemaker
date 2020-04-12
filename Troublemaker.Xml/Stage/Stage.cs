using System;
using System.Collections;
using System.Collections.Generic;
using Troublemaker.Framework;

namespace Troublemaker.Xml
{
    [XPath("self::stage")]
    public sealed class Stage : IExpandable, IStage
    {
        [XPath("@initialize")] public String Initialize;
        [XPath("@map")] public String Map;
        [XPath("@version")] public Int64 Version;

        [XPath("StartCamera")] public CameraPosition StartCamera;
        [XPath("Dashboards/*")] public StageDashboard[] Dashboards;
        [XPath("Functions/*")] public StageFunction[] Functions;
        [XPath("MapComponents/*/@Key")] public Map<StageMapComponent> MapComponents;
        [XPath("Objectives/*")] public StageCondition[] Objectives;
        [XPath("Relations/*")] public StageRelation[] Relations;
        [XPath("Triggers/*")] public StageTrigger[] Triggers;
        [XPath("Variables/*")] public StageVariable[] Variables;
        [XPath("MissionDirects/*")] public StageMissionDirect[] MissionDirects;

        public String NodeName => $"Stage ({Map})";

        public IEnumerable<(String name, IExpandable expandable)> EnumerateChildren()
        {
            yield return Dashboards.Named(nameof(Dashboards));
            yield return Functions.Named(nameof(Functions));
            yield return MapComponents.Named(nameof(MapComponents));
            yield return Objectives.Named(nameof(Objectives));
            yield return Triggers.Named(nameof(Triggers));
            yield return MissionDirects.Named(nameof(MissionDirects));
        }

        public Boolean TryResolveMapComponent(String objectId, out StageMapComponent mapComponent)
        {
            if (MapComponents is null)
            {
                mapComponent = default;
                return false;
            }
            
            return MapComponents.TryGetValue(objectId, out mapComponent);
        }
    }
}