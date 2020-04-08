using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='RestoreMaxHP']")]
    public sealed class StageActionRestoreMaxHP : StageAction
    {
        [XPath("Unit")] public StagePointObject Unit;
    }
}