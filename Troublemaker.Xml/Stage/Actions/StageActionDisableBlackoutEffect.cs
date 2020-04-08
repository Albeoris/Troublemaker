using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='DisableBlackoutEffect']")]
    public sealed class StageActionDisableBlackoutEffect : StageAction
    {
        [XPath("@Name")] public String Name;
    }
}