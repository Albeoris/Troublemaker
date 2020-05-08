using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='SetNamedAssetVisible']")]
    public sealed class DialogActionSetNamedAssetVisible : DialogAction
    {
        [XPath("@Key")] public String Key;
        [XPath("@Visible")] public Boolean Visible;
    }
}