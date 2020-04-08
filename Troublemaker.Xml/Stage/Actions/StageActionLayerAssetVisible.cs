using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='LayerAssetVisible']")]
    public sealed class StageActionLayerAssetVisible : StageAction
    {
        [XPath("@Name")] public String Name;
        [XPath("@Visible")] public Boolean Visible;
    }
}