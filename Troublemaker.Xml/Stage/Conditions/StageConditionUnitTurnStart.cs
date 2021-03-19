namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='UnitTurnStart']")]
    public sealed class StageConditionUnitTurnStart : StageCondition
    {
        [XPath("Unit")] public StagePointObject Unit;
    }
}