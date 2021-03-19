using System;

namespace Troublemaker.Xml
{
    [XPath("self::Variable[@Type='ExistUnit']")]
    public sealed class StageVariableExistUnit : StageVariable
    {
        [XPath("@Value")] public String Value;
        [XPath("Unit")] public StagePointObject Unit;
    }
}