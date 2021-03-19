namespace Troublemaker.Xml
{
    [XPath("self::StageDataBinding[@Type='Position']")]
    public sealed class StageDataBindingPosition : StageDataBinding
    {
        [XPath("PositionIndicator")] public StagePoint PositionIndicator;
    }
}