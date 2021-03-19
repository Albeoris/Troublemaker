using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='UnitRemoveBuff']")]
    public sealed class StageActionUnitRemoveBuff : StageAction
    {
        [XPath("@Name")] public String Name;
        [XPath("@Value")] public String Value;

        [XPath("Unit")] public StagePointObject Unit;
    }
}