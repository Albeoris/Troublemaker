using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='UpdateAchievement']")]
    public sealed class StageActionUpdateAchievement : StageAction
    {
        [XPath("@Achievement")] public String Achievement;
    }
}