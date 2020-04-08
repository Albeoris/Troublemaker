using System;

namespace Troublemaker.Xml
{
    [XPath("self::ActionInstance")]
    public sealed class StageActionInstance : StageAction
    {
        [XPath("@ActionType")] public String BaseCoverable;
    }
}