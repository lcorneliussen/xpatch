using System;
using System.Linq;
using System.Reflection;
using log4net;
using log4net.Config;
using log4net.Core;

namespace XPatch.Console
{
    public class XPatchRunner
    {
        private ILog _log = LogManager.GetLogger(typeof(XPatchRunner));

        private readonly IConsole _console;
        private IXmlXPathPatcher _patcher;

        public XPatchRunner(IConsole console,
                                    IXmlXPathPatcher patcher)
        {
            _patcher = patcher;
            _console = console;
        }

        public bool Run(XPatchOptions options)
        {
            BasicConfigurator.Configure(new XPatchConsoleAppender(_console));
            LogManager.GetRepository().Threshold = MapLevel(options.LogLevel);

            if (!options.NoLogo)
            {
                _console.Write(BuildLogo());
            }

            if (options.ShowHelp)
            {
                _console.Write(options.GetHelp());
                return true;
            }

            if (options.Invalid)
            {
                _console.Error("Ungültige Parameter! 'xpatch -?' zeigt die Hilfe an.");
                return false;
            }

            string newValue = options.NewValue
                                ?? _console.Prompt(
                                    string.Format("Neuer Wert für '{0}': ",
                                                options.XPathExpression));

            try
            {
                _patcher.Patch(options.XmlFile, options.XPathExpression, newValue);
                return true;
            }
            catch (Exception e)
            {
                _log.Error("Bei der Ausführung ist ein Fehler aufgetreten!", e);
                return false;
            }
        }

        private string BuildLogo()
        {
            Assembly assembly = GetType().Assembly;
            var attributes = assembly.GetCustomAttributes(false);

            string title = attributes.OfType<AssemblyTitleAttribute>()
                .Single().Title;

            string description = attributes.OfType<AssemblyDescriptionAttribute>()
                .Single().Description;

            string copyright = attributes.OfType<AssemblyCopyrightAttribute>()
                .Single().Copyright;

            Version assemblyVersion = assembly.GetName().Version;
            string version = assemblyVersion.Major
                                + "." + assemblyVersion.Minor;

            return string.Format("{0} {1} - {2}\n{3}\n",
                title, version, description, copyright);
        }

        private Level MapLevel(XPatchLogLevel level)
        {
            switch (level)
            {
                case XPatchLogLevel.Error:
                    return Level.Error;
                case XPatchLogLevel.Info:
                    return Level.Info;
                case XPatchLogLevel.Verbose:
                    return Level.All;
                default /*XPatchOptions.XPatchLogLevel.Off*/:
                    return Level.Off;
            }
        }
    }
}