namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='UpdateObjectPropertyEx']")]
    public sealed class StageActionUpdateObjectPropertyEx : StageAction
    {
        [XPath("StageDataBinding")] public StageDataBinding StageDataBinding;
        [XPath("Unit")] public StagePoint Unit;
    }
}