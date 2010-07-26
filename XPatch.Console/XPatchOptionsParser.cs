using System.IO;
using CommandLine;

namespace XPatch.Console
{
    public class XPatchOptionsParser
    {
        public XPatchOptions Parse(params string[] args)
        {
            var options = new XPatchOptions();
            var commandLineParser = new CommandLineParser();

            var helpWriter = new StringWriter();
            if (commandLineParser.ParseArguments(args, options, helpWriter)
                && options.Values.Count >= 2)
            {
                options.XmlFile = options.Values[0];
                options.XPathExpression = options.Values[1];

                if (options.Values.Count == 3)
                    options.NewValue = options.Values[2];
            }
            else
            {
                options.Invalid = true;
            }

            return options;
        }
    }
}