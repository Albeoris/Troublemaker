namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='UnitArrived']")]
    public sealed class StageConditionUnitArrived : StageCondition
    {
        [XPath("Area")] public StageArea Area;
        [XPath("Unit")] public StagePointObject Unit;
    }
}