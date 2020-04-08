using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='ToggleInteractionArea']")]
    public sealed class StageActionToggleInteractionArea : StageAction
    {
        [XPath("@InteractionAreaKey")] public String InteractionAreaKey;
    }
}