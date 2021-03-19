namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='ChangeTileEnterableEx']")]
    public sealed class StageActionChangeTileEnterableEx : StageAction
    {
        [XPath("Area")] public StageArea Area;
    }
}