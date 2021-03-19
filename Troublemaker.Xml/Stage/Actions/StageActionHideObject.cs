using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='HideObject']")]
    public sealed class StageActionHideObject : StageAction
    {
        [XPath("Unit")] public StagePointObject Unit;
    }
}