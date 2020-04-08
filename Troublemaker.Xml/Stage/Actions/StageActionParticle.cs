using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='Particle']")]
    public sealed class StageActionParticle : StageAction
    {
        [XPath("@AttachModel")] public Boolean AttachModel;
        [XPath("@DirectClear")] public Boolean DirectClear;
        [XPath("@Particle")] public String Particle;
        [XPath("@ParticleLength")] public Int64 ParticleLength;
        [XPath("@ParticlePos")] public String ParticlePos;

        [XPath("Unit")] public StagePointObject Unit;
    }
}