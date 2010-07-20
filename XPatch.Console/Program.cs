using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace XPatch.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            args = new string[] { @"..\..\test.xml", "xml/@attribut" /*, "wert2"*/};
            
            var xml = new XmlDocument();
            xml.Load(args[0]);
            // System.Console.WriteLine(xml.OuterXml);

            var newValue = args.Length > 2 ? args[2] : null;

            if (newValue == null)
            {
                System.Console.Write("Please enter the new value for '{0}':", args[1]);
                newValue = System.Console.ReadLine();
            }

            XmlNode element = xml.SelectSingleNode(args[1]);
            var oldValue = element.InnerText;
            element.InnerText = newValue;

            xml.Save(args[0]);
            System.Console.Write("Value for '{0}' changed from '{1}' to '{2}'.", args[1], oldValue, newValue);
            
            // System.Console.WriteLine(xml.OuterXml);

            // System.Console.ReadLine();
        }
    }
}
