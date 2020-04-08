using System;

namespace Troublemaker.Xml
{
    [XPath("self::StageDataBinding[@Type='StageVariable']")]
    public sealed class StageDataBindingVariable : StageDataBinding
    {
        [XPath("@Variable")] public String Variable;
    }
}