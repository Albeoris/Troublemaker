using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='HelpMessage']")]
    public sealed class StageActionHelpMessage : StageAction
    {
        [XPath("@HelpMessage")] public String HelpMessage;
        [XPath("@HelpType")] public String HelpType;
    }
}