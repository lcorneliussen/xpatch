namespace XPatch.Console
{
    public interface IXmlFileSource
    {
        string Load(string fileName);
        void Save(string fileName, string contents);
    }
}