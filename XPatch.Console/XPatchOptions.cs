using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace XPatch.Console
{
    public enum XPatchLogLevel { Verbose, Info, Error, Off }
    public class XPatchOptions
    {
        public bool Invalid;

        [Option("n", "nologo",
            HelpText = "Unterbindet die Anzeige der Programminformation")]
        public bool NoLogo;

        [Option("l", "loglevel",
            HelpText = "Gibt an, wie geschwätzig die Ausgabe sein soll."
                        + " Default: Info, alternativ: Verbose, Error oder Off")]
        public XPatchLogLevel LogLevel = XPatchLogLevel.Info;

        [Option("?", "help", HelpText = "Zeigt die Programmhilfe an")]
        public bool ShowHelp;

        // Catch all...
        [ValueList(typeof(List<string>), MaximumElements = 3)]
        public IList<string> Values;

        // Werden programmatisch aus Values befüllt
        public string XmlFile;
        public string XPathExpression;
        public string NewValue;

        public string GetHelp()
        {
            var help = new HelpText("  Benutzung: xpatch xmlDatei xpath-Ausdruck [neuerwert] [parameter]"
                                    + "\n\nWird kein neuer Wert angegeben, fordert xpatch zur "
                                    + "Eingabe des gewünschten Wertes auf."
                                    + "\n\nWeitere Parameter:");
            help.AddOptions(this);
            return help;
        }
    }
}