namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='ChangeTileEnterable']")]
    public sealed class StageActionChangeTileEnterable : StageAction
    {
        [XPath("Area")] public StageArea Area;
    }
}