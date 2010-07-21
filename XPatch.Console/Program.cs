using System.Diagnostics;
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

            var xml = new XmlDocument();
            xml.Load(args[0]);

            var newValue = args.Length > 2 ? args[2] : null;

            if (newValue == null)
            {
                C.Write("Please enter the new value for '{0}': ", args[1]);
                newValue = System.Console.ReadLine();
            }

            XmlNode element = xml.SelectSingleNode(args[1]);
            var oldValue = element.InnerText;
            element.InnerText = newValue;

            xml.Save(args[0]);
            C.Write("Value for '{0}' changed from '{1}' to '{2}'.",
                    args[1], oldValue, newValue);

#if (DEBUG)
            if (Debugger.IsAttached)
                C.ReadLine();
#endif
        }
    }
}