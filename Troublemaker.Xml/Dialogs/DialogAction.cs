using System;

namespace Troublemaker.Xml.Dialogs
{
    [XPath("self::property")]
    public abstract class DialogAction
    {
        public virtual void Translate(LocalizationTree tree)
        {
            throw new NotSupportedException(GetType().FullName);
        }
    }
}