namespace Troublemaker.Xml
{
    [XPath(".")]
    public struct StageArea
    {
        [XPath("From")] public StagePosition From;
        [XPath("To")] public StagePosition To;
    }
}