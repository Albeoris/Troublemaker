using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='ResetAbilityUseCountAll']")]
    public sealed class StageActionResetAbilityUseCountAll : StageAction
    {
        [XPath("Unit")] public StagePointObject Unit;
    }
}