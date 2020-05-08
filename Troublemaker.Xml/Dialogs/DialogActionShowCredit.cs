using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='ShowCredit']")]
    public sealed class DialogActionShowCredit : DialogAction
    {
        [XPath("@CreditType")] public String CreditType;
        [XPath("@Slow")] public Boolean Slow;
    }
}