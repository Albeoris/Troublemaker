using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='UpdateCompanyMissionProp']")]
    public sealed class StageActionUpdateCompanyMissionProp : StageAction
    {
        [XPath("PropKV")] public StageKeyValue PropKV;
    }
}