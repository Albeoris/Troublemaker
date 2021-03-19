using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='AddObjectProperty']")]
    public sealed class StageActionAddObjectProperty : StageAction
    {
        [XPath("PropKV")] public StageKeyValue PropKV;
        [XPath("Unit")] public StagePointObject Unit;
    }
}