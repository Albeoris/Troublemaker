using System;

namespace Troublemaker.Xml
{
    [XPath("self::class")]
    public sealed class XmlMasteryAbilityType
    {
        [XPath("@name")] public String Name;       // "FallWeb_Strong_Acid"
        [XPath("@Key")] public String Key;         // "FallWeb_Strong_Acid"
        [XPath("@Idspace")] public String Idspace; // "Ability"
        [XPath("@Short")] public Boolean Short;    // "false"
        [XPath("@Text")] public String Text;      // ""
    }
}