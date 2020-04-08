namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='UpdateAutoPlayable']")]
    public sealed class StageActionUpdateAutoPlayable : StageAction
    {
        [XPath("Unit")] public StagePointObject Unit;
    }
}