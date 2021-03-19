using System;
using System.Xml.XPath;
using Troublemaker.Framework;

namespace Troublemaker.Xml
{
    public sealed class XPathExpressionCache
    {
        public static XPathExpressionCache Instance { get; } = new XPathExpressionCache();

        private readonly Cache<XPathExpression> _cache = new Cache<XPathExpression>(XPathExpression.Compile);

        public XPathExpression Ensure(String xpath)
        {
            return _cache.Ensure(xpath);
        }
    }
}