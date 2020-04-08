using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='UpdateUserMember']")]
    public sealed class StageActionUpdateUserMember : StageAction
    {
        [XPath("@Team")] public String Team;

        [XPath("Unit")] public StagePointObject Unit;
    }
}