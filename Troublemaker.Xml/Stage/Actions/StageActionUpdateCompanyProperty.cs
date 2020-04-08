namespace Troublemaker.Xml
{
    [XPath("self::Action[@Type='UpdateCompanyProperty']")]
    public sealed class StageActionUpdateCompanyProperty : StageAction
    {
        [XPath("PropKV")] public StageKeyValue PropKV;
    }
}