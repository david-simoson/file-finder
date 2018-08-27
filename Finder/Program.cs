using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finder
{
    class Program
    {
        static void Main(string[] args)
        {
            var useRegex = false;

            if (args.Length < 1)
            {
                Console.WriteLine("You must pass a search string as an argument, Enter \"-help\" for more information on usage");
                Environment.Exit(0);
            }

            if (args.Contains("-help"))
            {
                Console.WriteLine(Help.HelpString);
                Environment.Exit(0);
            }

            if (args.Contains("-rgx"))
            {
                useRegex = true;
            }

            var finder = new Finder(args[0], useRegex);
            var dispay = new Display(finder);

            finder.Find();

        }
    }
}
