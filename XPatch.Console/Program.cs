using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace XPatch.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var xml = new XmlDocument();
            xml.Load(@"..\..\test.xml");
            System.Console.WriteLine(xml.OuterXml);
            xml.SelectSingleNode("xml/@attribut").InnerText = "wert2";
            System.Console.WriteLine(xml.OuterXml);
            System.Console.ReadLine();
        }
    }
}
