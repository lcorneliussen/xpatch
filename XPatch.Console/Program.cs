
using System;
using System.IO;

namespace XPatch.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var source = new XmlFileSource();
            var console = new SystemConsole();

            var patcher = new XmlXPathPatcher(source, console);
            var runner = new XPatchRunner(console, patcher);

            var options = new XPatchOptionsParser().Parse(args);

            bool success = runner.Run(options);
            Environment.Exit(success ? 0 : -1);
        }
    }

    public class SystemConsole : IConsole
    {
        public string Prompt(string message)
        {
            System.Console.Write(message);
            return System.Console.ReadLine();
        }

        public void Error(string errorMessage)
        {
            System.Console.Error.WriteLine(errorMessage);
        }

        public void Info(string message)
        {
            System.Console.WriteLine(message);
        }
    }

    public class XmlFileSource : IXmlFileSource
    {
        public string Load(string fileName)
        {
            return File.ReadAllText(fileName);
        }

        public void Save(string fileName, string contents)
        {
            File.WriteAllText(fileName, contents);
        }
    }
}