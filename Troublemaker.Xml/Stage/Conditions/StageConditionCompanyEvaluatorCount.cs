using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='CompanyEvaluatorCount']")]
    public sealed class StageConditionCompanyEvaluatorCount : StageCondition
    {
        [XPath("@Operation")] public String Operation;
        [XPath("@FailExpression")] public String FailExpression;
        [XPath("@SuccessExpression")] public String SuccessExpression;
        [XPath("@Value")] public String Value;
    }
}