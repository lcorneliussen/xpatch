namespace XPatch.Console
{
    public interface IXmlXPathPatcher
    {
        void Patch(string xmlFile, string xpathExpression, string newValue);
    }
}