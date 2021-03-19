using System;

namespace Troublemaker.Xml
{
    [XPath("self::SecretAgent")]
    public sealed class StageMapComponentSecretAgent : StageMapComponent
    {
        [XPath("@Object")] public String Object;

        [XPath("AI")] public StageAI AI;
        [XPath("Direction")] public StagePosition Direction;
        [XPath("Position")] public StagePosition Position;
        [XPath("SecretAgentType")] public StageMapComponentSecretAgentType SecretAgentType;
    }
}