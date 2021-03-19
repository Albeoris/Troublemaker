using System;
using System.Windows;
using System.Windows.Data;

namespace Troublemaker.Editor.Framework
{
    public class Ancestor : Binding
    {
        public Ancestor(String path, Type ancestorType)
        {
            Path = new PropertyPath(path);
            RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, ancestorType, 1);
        }
    }

    public class AncestorBinding : BindingDecoratorBase
    {
        public Type AncestorType { get; set; }
        public int AncestorLevel { get; set; }

        public AncestorBinding()
        {
            AncestorLevel = 1;
        }

        public AncestorBinding(string path)
            : base(path)
        {
            AncestorLevel = 1;
        }

        public override object ProvideValue(IServiceProvider provider)
        {
            if (RelativeSource == null)
                RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, AncestorType, AncestorLevel);

            return base.ProvideValue(provider);
        }
    }
}