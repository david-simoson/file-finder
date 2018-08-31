using System;
using System.Linq;
using System.Text;

namespace Finder
{
    static class Display
    {
        public static int totalSearched = 0;
        private static int numHits = 0;
        private static int errors = 0;

        public static void ShowProgress (int totalFiles)
        {
            ShowProgress(totalFiles, 0);
        }

        public static void ShowProgress (int totalFiles, int totalSearched)
        {
            ShowProgress(totalFiles, totalSearched, 0);
        }

        public static void ShowProgress (int totalFiles, int totalSearched, int numHits)
        {
            ShowProgress(totalFiles, totalSearched, numHits, 0);
        }

        public static void ShowProgress(int totalFiles, int totalSearched, int numHits, int errors)
        {
            if (totalSearched != 0)
                Display.totalSearched = totalSearched;

            if (numHits != 0)
                Display.numHits = numHits;

            if (errors != 0)
                Display.errors = numHits;

            var progressMessage = 
                string.Format("Total Files: {0}  Searched: {1}  Hits: {2}  Errors:  {3}",
                totalFiles,
                Display.totalSearched,
                Display.numHits,
                Display.errors);

            Console.Write("\r" + progressMessage);
        }

        public static void PrintSummary(string[] foundFiles, string[] errorFiles)
        {
            if (errorFiles.Length > 0)
            {
                Console.WriteLine("\r\n");

                Console.WriteLine("\nThe following files were unable to be searched and thus were skipped during the process: ");
                foreach (string errFile in errorFiles)
                {
                    Console.WriteLine(errFile);
                }

            }

            if (foundFiles.Count() == 0)
            {
                Console.WriteLine("\nNo files were found containing the search string");
                return;
            }

            Console.WriteLine("\r\n");

            Console.WriteLine("The following files contained the search string: ");
            foreach (string file in foundFiles)
            {
                Console.WriteLine(file);
            }
        }
    }
}
