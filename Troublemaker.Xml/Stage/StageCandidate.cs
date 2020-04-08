using System;

namespace Troublemaker.Xml
{
    [XPath("self::Candidate")]
    public sealed class StageCandidate
    {
        [XPath("@Priority")] public Int64 Priority;

        [XPath("UnitWithPosList/*")] public StageEntry[] UnitWithPosList;
    }
}