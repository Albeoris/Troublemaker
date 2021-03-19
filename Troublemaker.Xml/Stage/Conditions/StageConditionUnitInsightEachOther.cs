namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='UnitInsightEachOther']")]
    public sealed class StageConditionUnitInsightEachOther : StageCondition
    {
        [XPath("Unit")] public StagePointObject Unit;
        [XPath("Unit2")] public StagePointObject Unit2;
    }
}