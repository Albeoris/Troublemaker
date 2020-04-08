namespace Troublemaker.Xml
{
    [XPath("self::Entry")]
    public sealed class StageEntry
    {
        [XPath("LookPosition")] public StagePosition LookPosition;
        [XPath("Position")] public StagePosition Position;
        [XPath("Unit")] public StagePointObject Unit;
    }
}