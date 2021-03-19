using System;

namespace Troublemaker.Xml
{
    public sealed class LocalizeString
    {
        public TextId Key { get; }
        public String Text { get; set; }
        public String Comment { get; }

        public LocalizeString(TextId key, String text, String comment)
        {
            Key = key;
            Text = text;
            Comment = comment;
        }
    }
}