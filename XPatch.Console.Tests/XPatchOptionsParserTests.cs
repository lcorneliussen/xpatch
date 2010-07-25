using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTestsEx;

namespace XPatch.Console.Tests
{
    [TestClass]
    public class XPatchOptionsParserTests
    {
        private XPatchOptionsParser _parser;

        [TestInitialize]
        public void Setup()
        {
            _parser = new XPatchOptionsParser();
        }

        [TestMethod]
        public void NoOptions_IsInvalid()
        {
            _parser.Parse().Invalid.Should().Be.True();
        }

        [TestMethod]
        public void Questionmark_ShowHelpIsSet()
        {
            _parser.Parse("-?").ShowHelp.Should().Be.True();
        }

        [TestMethod]
        public void NoLogo_IsSet()
        {
            _parser.Parse("-nologo").NoLogo.Should().Be.True();
        }

        [TestMethod]
        public void FourArguments_IsInvalid()
        {
            _parser.Parse("1", "2", "3", "4").Invalid.Should().Be.True();
        }

        [TestMethod]
        public void TwoArguments_MappedCorrectly()
        {
            var opt = _parser.Parse("foo.xml", "bar");
            opt.XmlFile.Should().Be.EqualTo("foo.xml");
            opt.XPathExpression.Should().Be.EqualTo("bar");
            opt.NewValue.Should().Be.Null();
        }

        [TestMethod]
        public void ThreeArguments_MappedCorrectly()
        {
            var opt = _parser.Parse("foo.xml", "bar", "value");
            opt.XmlFile.Should().Be.EqualTo("foo.xml");
            opt.XPathExpression.Should().Be.EqualTo("bar");
            opt.NewValue.Should().Be.EqualTo("value");
        }
    }
}
