using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='TroublesumReward']")]
    public sealed class DialogActionTroublesumReward : DialogAction
    {
        [XPath("@Value")] public String Value;
    }
}