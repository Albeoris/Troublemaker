using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='SightSharing']")]
    public sealed class StageActionSightSharing : StageAction
    {
        [XPath("@Team")] public String Team;
        [XPath("@Visible")] public Boolean Visible;

        [XPath("Unit")] public StagePointObject Unit;
    }
}