namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='UnitDead']")]
    public sealed class StageConditionUnitDead : StageCondition
    {
        [XPath("Unit")] public StagePointObject Unit;
    }
}