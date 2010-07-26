using System.Xml;
using log4net;

namespace XPatch.Console
{
    public class XmlXPathPatcher : IXmlXPathPatcher
    {
        private ILog _log = LogManager.GetLogger(typeof(XmlXPathPatcher));
        private readonly IXmlFileSource _xmlFileSource;

        public XmlXPathPatcher(IXmlFileSource xmlFileSource)
        {
            _xmlFileSource = xmlFileSource;
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

            _log.InfoFormat("Wert für '{0}' wurde von '{1}' auf '{2}' geändert.",
                                xpathExpression, oldValue, newValue);
        }
    }
}