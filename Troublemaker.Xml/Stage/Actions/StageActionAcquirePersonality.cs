using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='AcquirePersonality']")]
    public sealed class StageActionAcquirePersonality : StageAction
    {
        [XPath("@RosterKey")] public String RosterKey;
        [XPath("@Personality")] public String Personality;
    }
}