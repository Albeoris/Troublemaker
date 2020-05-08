using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='SetNamedAssetVisibleAll']")]
    public sealed class DialogActionSetNamedAssetVisibleAll : DialogAction
    {
        [XPath("@Visible")] public Boolean Visible;
    }
}