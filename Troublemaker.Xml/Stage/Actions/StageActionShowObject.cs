using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='ShowObject']")]
    public sealed class StageActionShowObject : StageAction
    {
        [XPath("Unit")] public StagePointObject Unit;
    }
}