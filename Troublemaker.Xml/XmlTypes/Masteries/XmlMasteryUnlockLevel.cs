using System;

namespace Troublemaker.Xml
{
    [XPath("self::class")]
    public sealed class XmlMasteryUnlockLevel
    {
        [XPath("@name")] public String Name;                      // "AIModule"
        [XPath("@Title")] public String Title;                    // "인공지능"
        [XPath("@Color")] public String Color;                     // "Combine_Yellow"
        [XPath("Unlock/property/@value")] public String[] Levels; // 4, 9, 14, 19, 28, 38, 48
    }
}