using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='CameraPosition']")]
    public sealed class StageActionCameraPosition : StageAction
    {
        [XPath("@CameraAnimSlope")] public Double CameraAnimSlope;
        [XPath("@CameraDirectMove")] public Boolean CameraDirectMove;
        [XPath("@CameraFOV")] public Double CameraFOV;
        [XPath("@IncludeDead")] public Boolean IncludeDead;
        [XPath("@Time")] public Double Time;

        [XPath("PositionIndicator")] public StagePoint PositionIndicator;
        [XPath("UpdateDirection")] public StageUpdateDirection UpdateDirection;
    }
}