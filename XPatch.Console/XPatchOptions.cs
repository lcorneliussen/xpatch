namespace XPatch.Console
{
    public class XPatchOptions
    {
        public bool NoLogo { get; set; }
        public bool Invalid { get; set; }

        public bool ShowHelp { get; set; }

        public string XmlFile { get; set; }
        public string XPathExpression { get; set; }
        public string NewValue { get; set; }

        public string GetHelp()
        {
            return "Benutzung: xpatch xmlDatei xpath-Ausdruck [neuerWert] [-nologo]"
                    + "\n\nWird neuerWert nicht angegeben, fordert xpatch zur "
                    + "Eingabe des gewünschten Wertes auf.";
        }
    }
}