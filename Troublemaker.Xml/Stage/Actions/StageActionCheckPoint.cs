using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='CheckPoint']")]
    public sealed class StageActionCheckPoint : StageAction
    {
        [XPath("ActionInstance")] public StageActionInstance ActionInstance;

        public override IEnumerable<(String name, IExpandable expandable)> EnumerateChildren()
        {
            yield return ActionInstance.Named(nameof(ActionInstance));
        }
    }
}