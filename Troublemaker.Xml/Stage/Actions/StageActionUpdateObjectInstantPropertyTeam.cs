using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='UpdateObjectInstantPropertyTeam']")]
    public sealed class StageActionUpdateObjectInstantPropertyTeam : StageAction
    {
        [XPath("@EvalEach")] public Boolean EvalEach;
        [XPath("@Key")] public String Key;
        [XPath("Team")] public String Team;

        [XPath("StageDataBinding")] public StageDataBinding StageDataBinding;
    }
}