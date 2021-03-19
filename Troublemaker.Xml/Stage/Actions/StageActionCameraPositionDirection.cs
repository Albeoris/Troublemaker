using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='CameraPositionDirection']")]
    public sealed class StageActionCameraPositionDirection : StageAction
    {
        [XPath("@CamHideAssetDistance")] public Double CamHideAssetDistance;
        [XPath("@CameraAfterRelease")] public Boolean CameraAfterRelease;
        [XPath("@CameraAnimSlope")] public Double CameraAnimSlope;
        [XPath("@CameraDirectMove")] public Boolean CameraDirectMove;
        [XPath("@CameraFOV")] public Double CameraFOV;
        [XPath("@DOFEnable")] public Boolean DOFEnable;
        [XPath("@DOFFixDist")] public Boolean DOFFixDist;
        [XPath("@DOFFocusDist")] public Double DOFFocusDist;
        [XPath("@DOFInnerRange")] public Double DOFInnerRange;
        [XPath("@DOFOuterRange")] public Double DOFOuterRange;
        [XPath("@FogOfWarDisable")] public Boolean FogOfWarDisable;
        [XPath("@IncludeDead")] public Boolean IncludeDead;
        [XPath("@Time")] public Double Time;

        [XPath("CamPosDir")] public StageCamPosDir CamPosDir;
        [XPath("PositionIndicator")] public StagePoint PositionIndicator;
        [XPath("Unit")] public StagePointObject Unit;
        [XPath("UpdateDirection")] public StageUpdateDirection UpdateDirection;
    }
}