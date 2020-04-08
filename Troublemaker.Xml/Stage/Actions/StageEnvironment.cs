using System;

namespace Troublemaker.Xml
{
    [XPath("self::Variable")]
    public sealed class StageEnvironment
    {
        [XPath("@Name")] public String Name;

        [XPath("StageDataBinding")] public StageDataBinding StageDataBinding;
    }
}