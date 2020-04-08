namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='UnitInsightToUnit']")]
    public sealed class StageConditionUnitInsightToUnit : StageCondition
    {
        [XPath("SearchUnit")] public StagePointObject SearchUnit;
        [XPath("TargetUnit")] public StagePointObject TargetUnit;
    }
}