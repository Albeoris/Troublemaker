using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='LockCamera']")]
    public sealed class StageActionLockCamera : StageAction
    {
        [XPath("@CameraControlType")] public String CameraControlType;
    }
}