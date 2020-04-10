using System;
using  System.Collections.Generic;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::idspace[@id='Dialog']")]
    public sealed class Dialog : IStage
    {
        [XPath("class")] public DialogScript[] Scripts { get; set; }

        public void Translate(LocalizationTree tree)
        {
            tree = tree["Dialog"];
            foreach (var script in Scripts)
            {
                if (tree.TryGet(script.Name, out var child))
                    script.Translate(child);
            }
        }

        public Boolean TryResolveMapComponent(String objectId, out StageMapComponent mapComponent) => throw new NotSupportedException();

        public IEnumerable<(String name, IExpandable expandable)> EnumerateChildren()
        {
            yield return Scripts.Named(nameof(Scripts));
        }
    }
}