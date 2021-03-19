using System;
using System.Reflection;

namespace Troublemaker.Xml
{
    public sealed class ReflectionPropertySetter : IMemberSetter
    {
        private readonly PropertyInfo _propertyInfo;

        public ReflectionPropertySetter(PropertyInfo propertyInfo)
        {
            _propertyInfo = propertyInfo;
        }

        public void SetValue(Object target, Object value)
        {
            _propertyInfo.SetValue(target, value);
        }
    }
}