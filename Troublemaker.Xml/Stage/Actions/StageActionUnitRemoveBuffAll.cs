namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='UnitRemoveBuffAll']")]
    public sealed class StageActionUnitRemoveBuffAll : StageAction
    {
        [XPath("Unit")] public StagePointObject Unit;
    }
}