using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='MonsterLocator']")]
    public sealed class StageActionMonsterLocator : StageAction
    {
        [XPath("LocatorCandidate/*")] public StageCandidate[] LocatorCandidate;
    }
}