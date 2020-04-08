using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='ToogleRewardWhenResurrect']")]
    public sealed class StageActionToogleRewardWhenResurrect : StageAction
    {
        [XPath("Unit")] public StagePointObject Unit;
    }
}