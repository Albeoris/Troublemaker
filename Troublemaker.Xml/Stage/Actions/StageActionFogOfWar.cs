using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='FogOfWar']")]
    public sealed class StageActionFogOfWar : StageAction
    {
        [XPath("@Visible")] public Boolean Visible;
    }
}