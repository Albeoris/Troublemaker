using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='SightOff']")]
    public sealed class StageActionSightOff : StageAction
    {
        [XPath("@Name")] public String Name;
    }
}