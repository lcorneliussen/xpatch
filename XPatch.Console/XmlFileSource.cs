using System.IO;
using log4net;

namespace XPatch.Console
{
    public class XmlFileSource : IXmlFileSource
    {
        private static ILog _log = LogManager.GetLogger(typeof(XmlFileSource));
        public string Load(string fileName)
        {
            _log.DebugFormat("Datei '{0}' wird eingelesen", fileName);
            return File.ReadAllText(fileName);
        }

        public void Save(string fileName, string contents)
        {
            File.WriteAllText(fileName, contents);
            _log.DebugFormat("Datei '{0}' wurde gespeichert", fileName);
        }
    }
}