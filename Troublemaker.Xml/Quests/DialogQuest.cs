using System;
using System.Collections.Generic;

namespace Troublemaker.Xml.Quests
{
    [XPath("self::class[@name]")]
    public sealed class DialogQuest : IExpandable
    {
        [XPath("@name")] public String Name { get; set; }
        [XPath("Visit/property")] public DialogQuestsVisit[] Visit { get; set; }
        [XPath("Process/property")] public DialogQuestScript[] Process { get; set; }

        public void Translate(LocalizationTree tree)
        {
            if (tree.TryGet("Visit", out var visitTree))
            {
                foreach (var visit in Visit)
                {
                    if (visitTree.TryGet(visit.Name, out var child))
                        visit.Translate(child);
                }
            }
            
            if (tree.TryGet("Process", out var processTree))
            {
                foreach (var process in Process)
                {
                    if (processTree.TryGet(process.Name, out var child))
                        process.Translate(child);
                }
            }
        }

        public String NodeName => Name;

        public IEnumerable<(String name, IExpandable expandable)> EnumerateChildren()
        {
            foreach (var item in Visit)
                yield return (item.Name, item);

            foreach (var item in Process)
                yield return (item.Name, item);
        }
    }
}