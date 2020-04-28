using System;

namespace LeanHub.Shared.Helpers
{
    public interface IConsoleHelper
    {
        void WriteLine(string message);
    }

    public class ConsoleHelper : IConsoleHelper
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}