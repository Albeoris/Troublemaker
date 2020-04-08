namespace Troublemaker.Xml
{
    [XPath("self::CamPosDir")]
    public sealed class StageCamPosDir
    {
        [XPath("Direction")] public StagePosition Direction;
        [XPath("Position")] public StagePosition Position;
    }
}