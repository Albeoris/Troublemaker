using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='UpdateObjectState']")]
    public sealed class StageActionUpdateObjectState : StageAction
    {
        [XPath("@Base_Coverable")] public String BaseCoverable;
        [XPath("@IsHeadDisplay")] public String IsHeadDisplay;
        [XPath("@IsTurnDisplay")] public String IsTurnDisplay;
        [XPath("@Jumpable")] public String Jumpable;
        [XPath("@NotOccupy")] public String NotOccupy;
        [XPath("@Untargetable")] public String Untargetable;

        [XPath("Unit")] public StagePointObject Unit;
    }
}