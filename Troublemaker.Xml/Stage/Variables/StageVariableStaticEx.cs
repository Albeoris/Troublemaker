using System;

namespace Troublemaker.Xml
{
    [XPath("self::Variable[@Type='StaticEx']")]
    public sealed class StageVariableStaticEx : StageVariable
    {
        [XPath("StageDataBindingInit")] public StageDataBinding StageDataBindingInit;
    }
}