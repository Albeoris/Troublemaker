using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='NamedAssetVisible']")]
    public sealed class StageActionNamedAssetVisible : StageAction
    {
        [XPath("@Name")] public String Name;
        [XPath("@Visible")] public Boolean Visible;
    }
}