namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='UpdateDashboard2']")]
    public sealed class StageActionUpdateDashboard2: StageAction
    {
        [XPath("Command")] public StageCommand[] Command;
    }
}