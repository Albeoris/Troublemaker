using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::Interaction")]
    public sealed class StageMapComponentInteraction : StageMapComponent
    {
        [XPath("@Interaction")] public String Interaction;
        
        [XPath("ActionList/*")] public StageAction[] ActionList;
        
        [XPath("Direction")] public StagePosition Direction;
        [XPath("Position")] public StagePosition Position;
        
        public override IEnumerable<(String name, IExpandable expandable)> EnumerateChildren()
        {
            yield return ActionList.Named(nameof(ActionList));
        }
    }
}