using System;

namespace Troublemaker.Xml
{
    [XPath("self::Entry")]
    public sealed class StagePsionicStone
    {
        [XPath("@Prob")] public Int64 Prob;
        [XPath("@PsionicStoneType")] public String PsionicStoneType;
    }
}