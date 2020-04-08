using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='CompanyEvaluatorAll']")]
    public sealed class StageConditionCompanyEvaluatorAll : StageCondition
    {
        [XPath("@SuccessExpression")] public String SuccessExpression;
    }
}