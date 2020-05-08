using System;
using System.Collections.Generic;
using System.Linq;

namespace Troublemaker.Xml.Quests
{
    [XPath("self::idspace[@id='DialogQuest']")]
    public sealed class DialogQuests : IStage
    {
        [XPath("class")] public DialogQuest[] Quests { get; set; }

        public void Translate(LocalizationTree tree)
        {
            tree = tree["DialogQuest"];
            foreach (var quest in Quests)
            {
                if (tree.TryGet(quest.Name, out var child))
                    quest.Translate(child);
            }
        }

        public Boolean TryResolveMapComponent(String objectId, out StageMapComponent mapComponent) => throw new NotSupportedException();

        public IEnumerable<(String name, IExpandable expandable)> EnumerateChildren()
        {
            foreach (var child in Quests)
                yield return (child.Name, new ExpandableCollection(child.Name, child.EnumerateChildren().Select(c => c.expandable)));
        }
    }
}