namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='TimeElapse']")]
    public sealed class StageActionTimeElapse : StageAction
    {
        [XPath("StageDataBinding")] public StageDataBinding StageDataBinding;
    }
}