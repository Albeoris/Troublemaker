using System;
using System.Reflection;

namespace Troublemaker.Xml
{
    public sealed class ReflectionFieldSetter : IMemberSetter
    {
        private readonly FieldInfo _fieldInfo;

        public ReflectionFieldSetter(FieldInfo fieldInfo)
        {
            _fieldInfo = fieldInfo;
        }

        public void SetValue(Object target, Object value)
        {
            _fieldInfo.SetValue(target, value);
        }
    }
}