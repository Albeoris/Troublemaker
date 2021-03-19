using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='GuideAbility']")]
    public sealed class StageActionGuideAbility : StageAction
    {
        [XPath("@AbilityType")] public String AbilityType;
        [XPath("@CamDistance")] public Int64 CamDistance;
        [XPath("@Guide")] public Boolean Guide;
        [XPath("@HoldTime")] public Double HoldTime;
        [XPath("@MessageVisible")] public String MessageVisible;
        [XPath("@NoCamera")] public String NoCamera;
        [XPath("@NoLook")] public Boolean NoLook;
        [XPath("@NoTeamCheck")] public String NoTeamCheck;
        [XPath("@StatusVisible")] public String StatusVisible;

        [XPath("AbilityGuideFlag")] public StageAbilityGuideFlag AbilityGuideFlag;
        [XPath("PositionIndicator")] public StagePoint PositionIndicator;
        [XPath("ResultModifier")] public StageResultModifier ResultModifier;
        [XPath("Unit")] public StagePointObject Unit;
    }
}