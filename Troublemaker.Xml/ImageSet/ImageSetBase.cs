using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::images")]
    public sealed class ImageSetBase
    {
        [XPath("image/@name")]
        public Dictionary<String, ImageSetBaseImage> Images;

        public ImageSetBaseImage this[String name] => Images[name];
    }
}