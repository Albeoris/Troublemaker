namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='UnitDeadEvent']")]
    public sealed class StageConditionUnitDeadEvent : StageCondition
    {
        [XPath("Unit")] public StagePointObject Unit;
    }
}