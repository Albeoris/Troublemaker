using System;
using System.Collections.Generic;

namespace Troublemaker.Xml
{
    [XPath("self::class")]
    public sealed class XmlObject
    {
        [XPath("@name")] public String Name;
        [XPath("@Title")] public String TitleId;
        [XPath("@JobName")] public String JobName;
        [XPath("@FamilyName")] public String FamilyName;
        [XPath("@AgeType")] public String AgeType;
        [XPath("@Image")] public String Image;
        [XPath("@Image_Small")] public String Image_Small;
        [XPath("@Affiliation")] public String Affiliation;
        [XPath("@DialogTypingSound")] public String DialogTypingSound;

        [XPath("Emotions/*")] public XmlObjectInfoEmotion[] Emotions;
    }
}