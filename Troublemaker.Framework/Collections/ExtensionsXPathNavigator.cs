using System;
using System.Xml.XPath;

namespace Troublemaker.Framework
{
    public static class ExtensionsXPathNavigator
    {
        public static Boolean IsMatch(this XPathNavigator navigator, XPathExpression expression)
        {
            return navigator.Select(expression).MoveNext();
        }
    }
}