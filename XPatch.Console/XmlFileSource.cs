using System.IO;

namespace XPatch.Console
{
    public class XmlFileSource : IXmlFileSource
    {
        public string Load(string fileName)
        {
            return File.ReadAllText(fileName);
        }

        public void Save(string fileName, string contents)
        {
            File.WriteAllText(fileName, contents);
        }
    }
}