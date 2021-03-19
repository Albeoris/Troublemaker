using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='UnitAddBuff']")]
    public sealed class StageActionUnitAddBuff : StageAction
    {
        [XPath("@Name")] public String Name;
        [XPath("@Value")] public String Value;

        [XPath("Unit")] public StagePointObject Unit;
    }
}