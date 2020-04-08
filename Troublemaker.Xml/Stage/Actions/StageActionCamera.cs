using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='Camera']")]
    public sealed class StageActionCamera : StageAction
    {
        [XPath("@CameraAfterRelease")] public Boolean CameraAfterRelease;
        [XPath("@CameraAnimSlope")] public Double CameraAnimSlope;
        [XPath("@CameraDirectMove")] public Boolean CameraDirectMove;
        [XPath("@CameraFOV")] public Double CameraFOV;
        [XPath("@CameraKey")] public String CameraKey;
        [XPath("@IncludeDead")] public Boolean IncludeDead;
        [XPath("@Time")] public Double Time;

        [XPath("PositionIndicator")] public StagePoint PositionIndicator;
        [XPath("Unit")] public StagePointObject Unit;
        [XPath("UpdateDirection")] public StageUpdateDirection UpdateDirection;
    }
}