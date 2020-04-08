using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='Look']")]
    public sealed class StageActionLook : StageAction
    {
        [XPath("@Blink")] public Boolean Blink;

        [XPath("PositionIndicator")] public StagePoint PositionIndicator;
        [XPath("Unit")] public StagePointObject Unit;
    }
}