using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::Message")]
    public sealed class DialogMessage
    {
        [XPath("@Text")] public String Text;
        [XPath("@C_Name")] public String Name;

        public void Translate(LocalizationTree tree, out TextReference messageId)
        {
            if (tree.TryGet(nameof(Text), out var child))
                messageId = child.Value;
            else
                messageId = default;
        }
    }
}