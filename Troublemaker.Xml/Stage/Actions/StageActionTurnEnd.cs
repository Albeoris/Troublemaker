namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='TurnEnd']")]
    public sealed class StageActionTurnEnd : StageAction
    {
        [XPath("Unit")] public StagePointObject Unit;
    }
}