namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='UpdateObjectInstantProperty']")]
    public sealed class StageActionUpdateObjectInstantProperty : StageAction
    {
        [XPath("PropKV")] public StageKeyValue PropKV;
        [XPath("Unit")] public StagePointObject Unit;
    }
}