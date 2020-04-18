using System.Text;
using System.Windows.Data;

namespace Troublemaker.Editor.Framework
{
    public class SelfBinding : BindingDecoratorBase
    {
        public SelfBinding()
        {
            RelativeSource = new RelativeSource(RelativeSourceMode.Self);
        }

        public SelfBinding(string path)
            : base(path)
        {
            RelativeSource = new RelativeSource(RelativeSourceMode.Self);
        }
    }
}
