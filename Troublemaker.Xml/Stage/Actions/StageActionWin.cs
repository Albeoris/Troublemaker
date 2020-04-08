using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='Win']")]
    public sealed class StageActionWin : StageAction
    {
        [XPath("@Team")] public String Team;
        [XPath("@Value")] public String Value;
        [XPath("@Variable")] public String Variable;
    }
}