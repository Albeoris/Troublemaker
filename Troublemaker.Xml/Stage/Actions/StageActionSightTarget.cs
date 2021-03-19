using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='SightTarget']")]
    public sealed class StageActionSightTarget : StageAction
    {
        [XPath("@Range")] public Int64 Range;

        [XPath("Unit")] public StagePointObject Unit;
    }
}