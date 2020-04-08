using System;

namespace Troublemaker.Xml
{
    [XPath("self::Variable[@Type='Dummy']")]
    public sealed class StageVariableDummy : StageVariable
    {
    }
}