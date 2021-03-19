namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='UpdateObjectProperty']")]
    public sealed class StageActionUpdateObjectProperty : StageAction
    {
        [XPath("PropKV")] public StageKeyValue PropKV;
        [XPath("Unit")] public StagePointObject Unit;
    }
}