using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='UpdateStageVariableEx']")]
    public sealed class StageActionUpdateStageVariableEx : StageAction
    {
        [XPath("@Variable")] public String Variable;
        [XPath("StageDataBinding")] public StageDataBinding StageDataBinding;
    }
}