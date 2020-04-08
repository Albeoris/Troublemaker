using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Citizen")]
    public sealed class StageMapComponentCitizen : StageMapComponent, IStageMapUnit
    {
        [XPath("@CitizenType")] public String CitizenType;
        [XPath("@Object")] public String MonsterName { get; set; }

        [XPath("Direction")] public StagePosition Direction;
        [XPath("Position")] public StagePosition Position;
        [XPath("RetreatPosition")] public StagePosition RetreatPosition;
        [XPath("OnSuccessActionList/*")] public StageAction[] OnSuccessActionList;

        public override IEnumerable<(String name, IExpandable expandable)> EnumerateChildren()
        {
            yield return OnSuccessActionList.Named(nameof(OnSuccessActionList));
        }
    }
}