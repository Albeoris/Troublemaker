using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='ResetObject']")]
    public sealed class StageActionResetObject : StageAction
    {
        [XPath("@Cooldown")] public Boolean Cooldown;
        [XPath("@IncludeDead")] public Boolean IncludeDead;
        [XPath("@MaxCost")] public Boolean MaxCost;
        [XPath("@MaxHP")] public Boolean MaxHP;
        [XPath("@ResetSP")] public Boolean ResetSP;
        [XPath("@TurnEnd")] public Boolean TurnEnd;
        
        [XPath("Unit")] public StagePointObject Unit;
    }
}