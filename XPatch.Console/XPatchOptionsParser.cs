using System.Linq;

namespace XPatch.Console
{
    public class XPatchOptionsParser
    {
        public XPatchOptions Parse(params string[] args)
        {
            var options = new XPatchOptions();

            if (args.Contains("-nologo"))
            {
                args = args.Except(new[] { "-nologo" }).ToArray();
                options.NoLogo = true;
            }

            if (args.Contains("-?"))
            {
                options.ShowHelp = true;
                return options;
            }

            if (args.Length < 2 || args.Length > 3)
            {
                options.Invalid = true;
                return options;
            }

            options.XmlFile = args[0];
            options.XPathExpression = args[1];

            if (args.Length == 3)
                options.NewValue = args[2];

            return options;
        }
    }
}