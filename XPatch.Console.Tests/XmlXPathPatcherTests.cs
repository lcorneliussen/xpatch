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
            var console = new Mock<IConsole>();

            var factory = new Mock<IXmlFileSource>();
            factory.Setup(f => f.Load("abc.xml"))
                .Throws(new FileNotFoundException("Datei wurde nicht gefunden.", "abc.xml"));

            Executing.This(() => new XmlXPathPatcher(factory.Object, console.Object)
                .Patch("abc.xml", "", ""))
                .Should().Throw<FileNotFoundException>();

            // Aufruf erfolgt?
            factory.Verify(f => f.Load("abc.xml"));
        }

        [TestMethod]
        public void SimpleXml_ShouldReplaceValue()
        {
            var console = new Mock<IConsole>();

            var xmlFileSource = new MemoryXmlFileSource(
                new Dictionary<string, string> { { "abc.xml", "<xml attribute=\"Alt\" />" } });

            new XmlXPathPatcher(xmlFileSource, console.Object)
                .Patch("abc.xml", "/xml/@attribute", "Neuer Wert!");

            xmlFileSource.Load("abc.xml")
                .Should().Be.EqualTo("<xml attribute=\"Neuer Wert!\" />");
        }
    }
}
