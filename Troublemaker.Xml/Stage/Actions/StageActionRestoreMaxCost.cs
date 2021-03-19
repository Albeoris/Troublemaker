namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='RestoreMaxCost']")]
    public sealed class StageActionRestoreMaxCost : StageAction
    {
        [XPath("Unit")] public StagePointObject Unit;
    }
}