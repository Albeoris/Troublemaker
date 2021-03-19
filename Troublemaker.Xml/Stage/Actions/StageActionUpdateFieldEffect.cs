using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='UpdateFieldEffect']")]
    public sealed class StageActionUpdateFieldEffect : StageAction
    {
        [XPath("@FieldEffectType")] public String FieldEffectType;
        [XPath("Area")] public StageArea Area;
    }
}