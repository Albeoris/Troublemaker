namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='UnitArrivedToUnit']")]
    public sealed class StageConditionUnitArrivedToUnit : StageCondition
    {
        [XPath("SearchUnit")] public StagePointObject SearchUnit;
        [XPath("TargetUnit")] public StagePointObject TargetUnit;
        [XPath("Unit")] public StagePointObject Unit;
        [XPath("Unit2")] public StagePointObject Unit2;
    }
}