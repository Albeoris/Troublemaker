using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='ResetAbilityUseCount']")]
    public sealed class StageActionResetAbilityUseCount : StageAction
    {
        [XPath("@Ability")] public String Ability;

        [XPath("Unit")] public StagePointObject Unit;
    }
}