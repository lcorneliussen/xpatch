using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using C = System.Console;

namespace XPatch.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
#if (DEBUG)
            if (Debugger.IsAttached)
                args = new string[]
                           {
                               @"..\..\test.xml",
                               "xml/@attribut",
                               "wert2"
                           };
#endif
            if (args.Contains("-nologo"))
            {
                // Parameter "verbrauchen"
                args = args.Except(new[] { "-nologo" }).ToArray();
            }
            else
            {
                C.WriteLine("xpatch 0.2 - Ersetzt einzelne Werte in Xml-Dateien");
                C.WriteLine("Copyright (C) Lars Corneliussen 2010");
                C.WriteLine();
            }

            string help = "Benutzung: xpatch xmlDatei xpath-Ausdruck [neuerWert]"
                          + "\n\nWird neuerWert nicht angegeben, fordert xpatch zur "
                          + "Eingabe des gewünschten Wertes auf.";

            // Parameter überprüfen
            if (args.Length == 0)
            {
                // Hilfe als Fehlerausgabe
                C.Error.WriteLine(help);
                Environment.Exit(-1);
            }
            else if (args.Length == 1 && args[0] == "-?")
            {
                // Hilfe als Programmausgabe
                C.WriteLine(help);
                Environment.Exit(0);
            }
            if (args.Length < 2 || args.Length > 3)
            {
                // Hilfe als Fehlerausgabe
                C.Error.WriteLine("Ungültige Parameter! 'xpatch -?' zeigt die Hilfe an.");
                Environment.Exit(-1);
            }

            string xmlFile = args[0];
            string xpath = args[1];
            var newValue = args.Length > 2 ? args[2] : null;

            if (!File.Exists(xmlFile))
            {
                C.Error.WriteLine("Die Datei '{0}' existiert nicht.", xmlFile);
                Environment.Exit(-1);
            }

            try
            {
                var xml = new XmlDocument();
                xml.Load(xmlFile);

                if (newValue == null)
                {
                    C.Write("Please enter the new value for '{0}': ", xpath);
                    newValue = System.Console.ReadLine();
                }

                XmlNode element = xml.SelectSingleNode(xpath);
                var oldValue = element.InnerText;
                element.InnerText = newValue;

                xml.Save(xmlFile);
                C.Write("Value for '{0}' changed from '{1}' to '{2}'.",
                        xpath, oldValue, newValue);
            }
            catch (Exception e)
            {
                C.Error.WriteLine("Bei der Ausführung von xpatch ist ein Fehler aufgetreten: {0}\n", e.Message);
                Environment.Exit(-1);
            }

#if (DEBUG)
            if (Debugger.IsAttached)
                C.ReadLine();
#endif
        }
    }
}