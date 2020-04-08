using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='ChangeTileLink']")]
    public sealed class StageActionChangeTileLink : StageAction
    {
        [XPath("@UpdateLink")] public String UpdateLink;
        [XPath("@UpdateThrowing")] public String UpdateThrowing;
        [XPath("@UpdateVisible")] public String UpdateVisible;
        [XPath("Area")] public StageArea Area;
    }
}