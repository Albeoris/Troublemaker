using System;

namespace Troublemaker.Xml
{
    [XPath("self::Parameter")]
    public sealed class StageFunctionParameter
    {
        [XPath("@DataType")] public String DataType;
        [XPath("@Name")] public String Name;
    }
}