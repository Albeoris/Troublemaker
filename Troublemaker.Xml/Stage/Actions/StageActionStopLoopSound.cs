using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='StopLoopSound']")]
    public sealed class StageActionStopLoopSound : StageAction
    {
        [XPath("@Name")] public String Name;
    }
}