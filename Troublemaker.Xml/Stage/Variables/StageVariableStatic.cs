using System;

namespace Troublemaker.Xml
{
    [XPath("self::Variable[@Type='Static']")]
    public sealed class StageVariableStatic : StageVariable
    {
        [XPath("@Value")] public String Value;
    }
}