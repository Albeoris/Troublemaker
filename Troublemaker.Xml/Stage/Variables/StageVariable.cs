using System;

namespace Troublemaker.Xml
{
    [XPath("self::Variable")]
    public abstract class StageVariable
    {
        [XPath("@Key")] public String Key;
    }
}