using System;

namespace XPatch.Console
{
    public class XPatchRunner
    {
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
            if (!options.NoLogo)
            {
                _console.Info("xpatch 0.3 - Ersetzt einzelne Werte in Xml-Dateien"
                                + "\nCopyright (C) Lars Corneliussen 2010"
                                + "\n");
            }

            if (options.ShowHelp)
            {
                _console.Info(options.GetHelp());
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
                _console.Error(string.Format("Bei der Ausführung von xpatch ist ein Fehler aufgetreten: {0}\n",
                                                e.Message));
                return false;
            }
        }
    }
}