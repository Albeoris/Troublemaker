using System;

namespace Troublemaker.Xml
{
    [XPath("self::SecretAgentType")]
    public sealed class StageMapComponentSecretAgentType
    {
        [XPath("@Type")] public String Type;
    }
}