namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='RestoreMaxSP']")]
    public sealed class StageActionRestoreMaxSP : StageAction
    {
        [XPath("Unit")] public StagePointObject Unit;
    }
}