using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='UpdateTemporaryMastery']")]
    public sealed class StageActionUpdateTemporaryMastery : StageAction
    {
        [XPath("@Mastery")] public String Mastery;
        [XPath("@Level")] public Int64 Level;
        [XPath("@Count")] public Int64 Count;

        [XPath("Unit")] public StagePointObject Unit;
    }
}