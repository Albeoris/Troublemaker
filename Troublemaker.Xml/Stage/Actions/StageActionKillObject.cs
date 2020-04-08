using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='KillObject']")]
    public sealed class StageActionKillObject : StageAction
    {
        [XPath("Unit")] public StagePointObject Unit;
    }
}