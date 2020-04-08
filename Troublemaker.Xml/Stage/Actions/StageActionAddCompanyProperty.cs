using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='AddCompanyProperty']")]
    public sealed class StageActionAddCompanyProperty : StageAction
    {
        [XPath("PropKV")] public StageKeyValue PropKV;
    }
}