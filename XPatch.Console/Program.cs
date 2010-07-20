using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml;

namespace XPatch.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            args = new string[] { @"..\..\test.xml", "xml/@attribut", "wert2"};
            
            var xml = new XmlDocument();
            xml.Load(args[0]);
            System.Console.WriteLine(xml.OuterXml);
            xml.SelectSingleNode(args[1]).InnerText = args[2];
            System.Console.WriteLine(xml.OuterXml);

            System.Console.ReadLine();
        }
    }
}
