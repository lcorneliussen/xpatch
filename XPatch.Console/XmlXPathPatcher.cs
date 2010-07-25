using System.Xml;

namespace XPatch.Console
{
    public class XmlXPathPatcher : IXmlXPathPatcher
    {
        private readonly IXmlFileSource _xmlFileSource;
        private readonly IConsole _console;

        public XmlXPathPatcher(IXmlFileSource xmlFileSource, IConsole console)
        {
            _xmlFileSource = xmlFileSource;
            _console = console;
        }

        public void Patch(string xmlFile, string xpathExpression, string newValue)
        {
            var xmlString = _xmlFileSource.Load(xmlFile);

            var xml = new XmlDocument { PreserveWhitespace = true };
            xml.LoadXml(xmlString);

            XmlNode element = xml.SelectSingleNode(xpathExpression);
            var oldValue = element.InnerText;
            element.InnerText = newValue;

            _xmlFileSource.Save(xmlFile, xml.OuterXml);

            _console.Info(
                string.Format("Wert für '{0}' wurde von '{1}' auf '{2}' geändert.",
                                xpathExpression, oldValue, newValue));
        }
    }
}