using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='Sleep']")]
    public sealed class StageActionSleep : StageAction
    {
        [XPath("@Time")] public Double Time;
    }
}