using System;

namespace Troublemaker.Xml
{
    [XPath("self::class")]
    public sealed class XmlStatusType
    {
        [XPath("@name")] public String Name; // "Attack"
        [XPath("@Color")] public String Color; // "Combine_Red"
        [XPath("@TagImage")] public String TagImage; // "Masteryboard/AttackBar"
        [XPath("@Image")] public String Image; // "Icons/Weapon"
    }
}