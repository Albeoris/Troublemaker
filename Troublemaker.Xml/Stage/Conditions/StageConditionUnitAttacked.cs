using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='UnitAttacked']")]
    public sealed class StageConditionUnitAttacked : StageCondition
    {
        [XPath("@CheckKiller")] public Boolean CheckKiller;
        [XPath("@Relation")] public String Relation;
        
        [XPath("Unit")] public StagePointObject Unit;
    }
}