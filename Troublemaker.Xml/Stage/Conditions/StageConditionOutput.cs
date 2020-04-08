using System;

namespace Troublemaker.Xml
{
    [XPath("self::ConditionOutput")]
    public sealed class StageConditionOutput
    {
        [XPath("@Finder")] public String Finder;
        [XPath("@Team")] public String Team;
        [XPath("@Attacker")] public String Attacker;
        [XPath("@AttackerState")] public String AttackerState;
        [XPath("@Damage")] public String Damage;
        [XPath("@DefenderState")] public String DefenderState;
        [XPath("@SearchUnit")] public String SearchUnit;
        [XPath("@TargetUnit")] public String TargetUnit;
        [XPath("@Beast")] public String Beast;
        [XPath("@Tamer")] public String Tamer;

        [XPath("@Unit")] public String Unit;
        [XPath("@Unit1")] public String Unit1;
        [XPath("@Unit2")] public String Unit2;
    }
}