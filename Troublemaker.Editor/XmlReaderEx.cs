using System;
using System.Collections.Generic;
using System.Xml;

namespace Troublemaker.Editor
{
    public static class XmlReaderEx
    {
        public static IEnumerable<XmlReader> ReadChildren(this XmlReader reader)
        {
            if (reader.NodeType != XmlNodeType.Element)
                throw new ArgumentException(reader.NodeType.ToString(), nameof(reader));

            Int32 depth = reader.Depth;
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Depth == depth+1)
                {
                    var sub = reader.ReadSubtree();
                    if (sub.Read())
                        yield return sub;
                }
                else if (reader.NodeType == XmlNodeType.EndElement && reader.Depth == depth)
                {
                    yield break;
                }
            }
        }
    }
}