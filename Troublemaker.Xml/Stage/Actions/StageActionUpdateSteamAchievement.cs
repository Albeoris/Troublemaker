using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='UpdateSteamAchievement']")]
    public sealed class StageActionUpdateSteamAchievement : StageAction
    {
        [XPath("@SteamAchievement")] public String SteamAchievement;
    }
}