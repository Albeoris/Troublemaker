namespace Troublemaker.Xml
{
    [XPath("self::*[@Type='Position']")]
    public sealed class StagePointPosition : StagePoint
    {
        [XPath("Position")] public StagePosition Position;
    }
}