using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='GiveAbility']")]
    public sealed class StageActionGiveAbility : StageAction
    {
        [XPath("@Ability")] public String Ability;
        [XPath("Unit")] public StagePointObject Unit;
    }
}