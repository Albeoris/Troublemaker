using System;
using System.Collections.Generic;
using Troublemaker.Framework;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property")]
    public abstract class DialogAction : IExpandable
    {
        public virtual String NodeName => GetType().Name.TrimPrefix(nameof(DialogAction));
        public virtual IEnumerable<(String name, IExpandable expandable)> EnumerateChildren() => Array.Empty<(String name, IExpandable expandable)>();
        public virtual void Translate(LocalizationTree tree, DialogScript dialogScript, Dialog dialog) => throw new NotSupportedException(GetType().FullName);
    }
}