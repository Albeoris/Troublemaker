using System;

namespace Troublemaker.Xml
{
    [XPath("self::GameMessageForm")]
    public sealed class StageGameMessageForm
    {
        [XPath("@State")] public String State;
        [XPath("@Type")] public String Type;
        [XPath("@Who")] public String Who;
        [XPath("@Who2")] public String Who2;
    }
}