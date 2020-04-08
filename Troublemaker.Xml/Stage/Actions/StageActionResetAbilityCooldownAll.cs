namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='ResetAbilityCooldownAll']")]
    public sealed class StageActionResetAbilityCooldownAll : StageAction
    {
        [XPath("Unit")] public StagePointObject Unit;
    }
}