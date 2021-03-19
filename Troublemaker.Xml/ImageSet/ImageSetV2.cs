using System;
using System.Collections.Generic;
using System.Text;
using Troublemaker.Framework;

namespace Troublemaker.Xml
{
    [XPath("self::Imageset[@version='2']")]
    public sealed class ImageSetV2
    {
        [XPath("@name")] public String Name;
        [XPath("@version")] public Int64 Version;
        [XPath("@autoScaled")] public String AutoScaled;
        [XPath("@imagefile")] public String ImageFileName;
        [XPath("@nativeHorzRes")] public Int64 NativeWidth;
        [XPath("@nativeVertRes")] public Int64 NativeHeight;

        [XPath("Image/@name")] public Map<ImageSetV2Image> Images;
    }
}
