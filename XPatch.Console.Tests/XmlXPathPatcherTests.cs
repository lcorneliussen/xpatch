using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpTestsEx;
using XPatch.Console.Tests.Helper;

namespace XPatch.Console.Tests
{
    [TestClass]
    public class XmlXPathPatcherTests
    {
        [TestMethod]
        public void FileDoesNotExist_ShouldFail()
        {
            var factory = new Mock<IXmlFileSource>();
            factory.Setup(f => f.Load("abc.xml"))
                .Throws(new FileNotFoundException("Datei wurde nicht gefunden.", "abc.xml"));

            Executing.This(() => new XmlXPathPatcher(factory.Object)
                .Patch("abc.xml", "", ""))
                .Should().Throw<FileNotFoundException>();

            // Aufruf erfolgt?
            factory.Verify(f => f.Load("abc.xml"));
        }

        [TestMethod]
        public void SimpleXml_ShouldReplaceValue()
        {
            var xmlFileSource = new MemoryXmlFileSource(
                new Dictionary<string, string> { { "abc.xml", "<xml attribute=\"Alt\" />" } });

            new XmlXPathPatcher(xmlFileSource)
                .Patch("abc.xml", "/xml/@attribute", "Neuer Wert!");

            xmlFileSource.Load("abc.xml")
                .Should().Be.EqualTo("<xml attribute=\"Neuer Wert!\" />");
        }
    }
}
