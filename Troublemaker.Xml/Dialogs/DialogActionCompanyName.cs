using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property[@Type='CompanyName']")]
    public sealed class DialogActionCompanyName : DialogAction
    {
        [XPath("@Title")] public String Title;
        [XPath("@C_Hint")] public String Hint;
        [XPath("@Validation")] public String Validation;
        [XPath("@MaxLength")] public Int64 MaxLength;
        [XPath("@NameTarget")] public String NameTarget;
        [XPath("@MasteryTarget")] public String MasteryTarget;
    }
}