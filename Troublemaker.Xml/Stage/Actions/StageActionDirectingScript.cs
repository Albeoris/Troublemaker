using System;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='DirectingScript']")]
    public sealed class StageActionDirectingScript: StageAction
    {
        [XPath("@Script")] public String Script;
        [XPath("@DirScript")] public String DirScript;
    }
}