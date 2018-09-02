using System;

namespace Finder
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("You must pass a search string as an argument, Enter " + Args.Help[1] + " for more information on usage");
                Environment.Exit(0);
            }

            var finder = new Finder(args);
        }
    }
}
