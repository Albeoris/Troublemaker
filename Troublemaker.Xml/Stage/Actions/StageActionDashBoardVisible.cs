using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='DashBoardVisible']")]
    public sealed class StageActionDashBoardVisible : StageAction
    {
        [XPath("@Visible")] public Boolean Visible;
    }
}