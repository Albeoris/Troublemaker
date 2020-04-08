using System;

namespace Troublemaker.Xml
{
    [XPath("self::StageDataBinding[@Type='Expr']")]
    public sealed class StageDataBindingExpr : StageDataBinding
    {
        [XPath("@TestExpression")] public String TestExpression;
    }
}