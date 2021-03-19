namespace Troublemaker.Xml
{
    [XPath("self::Route")]
    public class StageRoute
    {
        [XPath("Position")] public StagePosition Position;
    }
}