namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='UpdateDashboard']")]
    public sealed class StageActionUpdateDashboard: StageAction
    {
        [XPath("Command")] public StageCommand Command;
    }
}