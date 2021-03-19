using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='MissionVisualArea']")]
    public sealed class StageActionMissionVisualArea : StageAction
    {
        [XPath("@DirectClear")] public Boolean DirectClear;
        [XPath("@Particle")] public String Particle;
        [XPath("@ParticleKey")] public String ParticleKey;
        [XPath("@ParticleLength")] public Int64 ParticleLength;

        [XPath("PositionIndicator")] public StagePoint PositionIndicator;
    }
}