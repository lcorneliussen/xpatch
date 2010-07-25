using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpTestsEx;

namespace XPatch.Console.Tests
{
    [TestClass]
    public class XPatchRunnerTests
    {
        private Mock<IXmlXPathPatcher> _patcher;
        private Mock<IConsole> _console;
        private XPatchRunner _runner;
        private XPatchOptions _options;
        private string _infoLog, _errorLog;

        [TestInitialize]
        public void Setup()
        {
            _patcher = new Mock<IXmlXPathPatcher>();
            _console = new Mock<IConsole>();

            _options = new XPatchOptions();
            _runner = new XPatchRunner(_console.Object, _patcher.Object);

            _console.Setup(c => c.Info(It.IsAny<string>()))
                .Callback<string>(s => _infoLog += s);

            _console.Setup(c => c.Error(It.IsAny<string>()))
                .Callback<string>(s => _errorLog += s);
        }

        [TestMethod]
        public void InvalidOptions_PrintsError()
        {
            _options.Invalid = true;

            _runner.Run(_options).Should().Be.False();

            _errorLog.Should().Contain("xpatch -?");
        }

        [TestMethod]
        public void ShowHelp_PrintsHelp()
        {
            _options.ShowHelp = true;

            _runner.Run(_options).Should().Be.True();

            _infoLog.Should().Contain(_options.GetHelp());
        }

        [TestMethod]
        public void NormalRun_PrintsLogo()
        {
            _options.XmlFile = "bar.xml";
            _options.XPathExpression = "foo";

            _runner.Run(_options).Should().Be.True();

            _infoLog.Should().Contain("xpatch 0.3")
                .And.Contain("Copyright (C)");
        }

        [TestMethod]
        public void NoLogo_SupressesLogo()
        {
            _options.XmlFile = "bar.xml";
            _options.XPathExpression = "foo";
            _options.NoLogo = true;

            _runner.Run(_options).Should().Be.True();

            _infoLog.Should().Not.Contain("xpatch 0.3")
                .And.Not.Contain("Copyright (C)");
        }

        [TestMethod]
        public void NoNewValue_PromptsAndPatches()
        {
            _console.Setup(c => c.Prompt(It.IsAny<string>()))
                .Returns("Neuer Wert!");

            _options.XmlFile = "bar.xml";
            _options.XPathExpression = "foo";
            _runner.Run(_options).Should().Be.True();

            // Erwartet aufrufe erfolgt?
            _console.Verify(c => c.Prompt(It.IsAny<string>()));
            _patcher.Verify(p => p.Patch("bar.xml", "foo", "Neuer Wert!"));
        }

        [TestMethod]
        public void PatcherFails_ReturnsFalseAndPrintsError()
        {
            _patcher.Setup(p => p.Patch(It.IsAny<string>(),
                                        It.IsAny<string>(),
                                        It.IsAny<string>()))
                                        .Throws(new Exception("Fehler-1234"));

            _options.XmlFile = "bar.xml";
            _options.XPathExpression = "foo";
            _options.NewValue = "new";
            _runner.Run(_options).Should().Be.False();

            // Erwartet aufrufe erfolgt?
            _errorLog.Contains("Fehler-1234");
        }
    }
}