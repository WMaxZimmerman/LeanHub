using NTrospection.CLI.Core;

namespace LeanHub.Console
{
   class Program
   {
       static void Main(string[] args)
       {
           new Processor().ProcessArguments(args);
       }
   }
}
