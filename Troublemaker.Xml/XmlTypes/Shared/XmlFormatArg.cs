using System;

namespace Troublemaker.Xml
{
    [XPath("self::property")]
    public sealed class XmlFormatArg
    {
        [XPath("@FormatKey")] public String FormatKey; // "CounterAttack"
        [XPath("@Color")] public String Color;         // "White"
        [XPath("@Idspace")] public String Idspace;     // "None"
        [XPath("@Key")] public String Key;             // "None"
        [XPath("@Value")] public String Value;         // "반격 공격"
        [XPath("@ValueType")] public String ValueType; // "text"

        public TextReference ValueId;

        public void Translate(LocalizationTree result)
        {
            if (result.TryGet(nameof(Value), out var value))
                ValueId = value.Value;
        }
    }
}