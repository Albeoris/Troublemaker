using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='UnitSetPos']")]
    public sealed class StageActionUnitSetPos : StageAction
    {
        [XPath("Position")] public StagePosition Position;
        [XPath("Unit")] public StagePointObject Unit;
    }
}