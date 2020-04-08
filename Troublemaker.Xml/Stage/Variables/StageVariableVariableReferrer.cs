using System;

namespace Troublemaker.Xml
{
    [XPath("self::Variable[@Type='VariableReferrer']")]
    public sealed class StageVariableVariableReferrer : StageVariable
    {
        [XPath("@StageVarExpr")] public String StageVarExpr;
        [XPath("@Value")] public String Value;
        [XPath("StageDataBindingInit")] public StageDataBinding StageDataBindingInit;
        [XPath("Referrer/Variable/@Variable")] public String[] Variables;
    }
}