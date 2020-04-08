using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='ExcludeUnit']")]
    public sealed class StageActionExcludeUnit : StageAction
    {
        [XPath("Unit")] public StagePointObject Unit;
    }
}