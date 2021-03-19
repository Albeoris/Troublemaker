using System;

namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='ActionDelimiter']")]
    public sealed class StageConditionActionDelimiter : StageCondition
    {
    }
}