using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='ChangeTileLinkEx']")]
    public sealed class StageActionChangeTileLinkEx : StageAction
    {
        [XPath("@UpdateLink")] public String UpdateLink;
        [XPath("@UpdateThrowing")] public String UpdateThrowing;
        [XPath("@UpdateVisible")] public String UpdateVisible;

        [XPath("AreaIndicator")] public StageAreaIndicator AreaIndicator;
    }
}