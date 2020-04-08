namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='Area']")]
    public sealed class StageAreaIndicator
    {
        [XPath("Area")] public StageArea Area;
    }
}