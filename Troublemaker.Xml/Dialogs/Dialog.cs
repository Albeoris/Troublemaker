namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::idspace[@id='Dialog']")]
    public sealed class Dialog
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
    }
}