using System;
using System.Xml.XPath;

namespace Troublemaker.Xml
{
    using static AttributeTargets;

    [AttributeUsage(Property | Field | Class | Struct)]
    public sealed class XPathAttribute : Attribute
    {
        public String XPath { get; }

        public XPathAttribute(String xpath)
        {
            XPath = xpath;
        }

        public XPathExpression Expression => XPathExpressionCache.Instance.Ensure(XPath);
    }
}