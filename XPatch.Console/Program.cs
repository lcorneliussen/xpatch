
using System;

namespace XPatch.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var source = new XmlFileSource();
            var console = new SystemConsole();

            var patcher = new XmlXPathPatcher(source);
            var runner = new XPatchRunner(console, patcher);

            var options = new XPatchOptionsParser().Parse(args);

            bool success = runner.Run(options);
            Environment.Exit(success ? 0 : -1);
        }
    }
}