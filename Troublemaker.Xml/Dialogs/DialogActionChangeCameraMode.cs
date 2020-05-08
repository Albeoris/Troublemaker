using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='ChangeCameraMode']")]
    public sealed class DialogActionChangeCameraMode : DialogAction
    {
        [XPath("@C_CameraMode")] public String CCameraMode;
        [XPath("@Direct")] public Boolean Direct;
    }
}