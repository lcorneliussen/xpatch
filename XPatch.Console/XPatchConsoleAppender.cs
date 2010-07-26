using System.IO;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;

namespace XPatch.Console
{
    public class XPatchConsoleAppender : AppenderSkeleton
    {
        private readonly IConsole _console;
        private readonly PatternLayout _infoLayout;
        private readonly PatternLayout _errorLayout;

        public XPatchConsoleAppender(IConsole console)
        {
            _console = console;
            Layout = new PatternLayout("[%-5level] %date{mm:ss,fff} %message (%logger)");
            _infoLayout = new PatternLayout("%message (%30.30logger)");
            _errorLayout = new PatternLayout("[%-5level] %date{mm:ss,fff} %message (%logger)%newline%exception");
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            if (loggingEvent.Level == Level.Error
                || loggingEvent.Level == Level.Fatal)
            {
                _console.Error(Render(loggingEvent, _errorLayout));
            }
            else if (loggingEvent.Level >= Level.Info)
            {
                _console.Write(Render(loggingEvent, _infoLayout));
            }
            else
            {
                string message = Render(loggingEvent, Layout);
                if (loggingEvent.ExceptionObject != null)
                    message += "\n" + loggingEvent.GetExceptionString();
                _console.Write(message);
            }
        }

        private string Render(LoggingEvent loggingEvent, ILayout layout)
        {
            var m = new StringWriter();
            layout.Format(m, loggingEvent);
            return m.ToString();
        }
    }
}