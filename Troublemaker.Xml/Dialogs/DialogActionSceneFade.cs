using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='SceneFade']")]
    public sealed class DialogActionSceneFade : DialogAction
    {
        [XPath("@Direct")] public Boolean Direct;
        [XPath("@FadeType")] public String FadeType;
    }
}