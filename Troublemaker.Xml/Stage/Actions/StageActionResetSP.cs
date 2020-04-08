using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='ResetSP']")]
    public sealed class StageActionResetSP : StageAction
    {
        [XPath("Unit")] public StagePointObject Unit;
    }
}