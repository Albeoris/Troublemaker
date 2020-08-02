using System;

namespace Troublemaker.Xml
{
    [XPath("self::property")]
    public sealed class XmlFormatText
    {
        [XPath("@CaseColor")] public String CaseColor; // "LimeGold"
        [XPath("@CaseType")] public String CaseType; // "None"
        [XPath("@CaseValueType")] public String CaseValueType; // "string"
        [XPath("@CaseValue")] public String CaseValue; // ""
        [XPath("@CaseLineBreak")] public Boolean CaseLineBreak; // "false"
        [XPath("@LineBreak")] public Boolean LineBreak; // "true"
        [XPath("@Text")] public String Text; // "적 공격에 대해 $CounterAttack$을 시도할 경우, 다음과 같은 효과가 발생합니다."

        [XPath("FormatKeyword/property")] public XmlFormatArg[] Args;

        public TextReference TextId;
        public TextReference CaseValueId;

        public void Translate(LocalizationTree tree)
        {
            LocalizationTree child;

            if (tree.TryGet(nameof(Text), out child))
                TextId = child.Value;
            
            if (tree.TryGet(nameof(CaseValue), out child))
                CaseValueId = child.Value;

            if (tree.TryGet("FormatKeyword", out child))
            {
                for (Int32 i = 0; i < Args.Length; i++)
                {
                    if (child.TryGet(i, out LocalizationTree value))
                        Args[i].Translate(value);
                }
            }
        }
    }
}