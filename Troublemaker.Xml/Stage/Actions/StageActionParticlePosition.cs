using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='ParticlePosition']")]
    public sealed class StageActionParticlePosition : StageAction
    {
        [XPath("@DirectClear")] public Boolean DirectClear;
        [XPath("@Particle")] public String Particle;
        [XPath("@ParticleLength")] public Int64 ParticleLength;

        [XPath("PositionIndicator")] public StagePoint PositionIndicator;
    }
}