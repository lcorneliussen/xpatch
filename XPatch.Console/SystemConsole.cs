namespace XPatch.Console
{
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
}