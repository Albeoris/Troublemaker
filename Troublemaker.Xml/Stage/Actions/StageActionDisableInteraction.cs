using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='DisableInteraction']")]
    public sealed class StageActionDisableInteraction : StageAction
    {
        [XPath("@InvestigateComputer")] public String InvestigateComputer;
        [XPath("@WareHouse_A0")] public String WareHouse_A0;
    }
}