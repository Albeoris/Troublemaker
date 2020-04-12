namespace Troublemaker.Xml
{
    public interface IStageSpeaker
    {
        public StageSpeaker Resolve(IStage stage);
    }
}