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

        /// <summary>
        /// Prints a progress line to the console
        /// </summary>
        /// <param name="totalFiles">The total number of files included in the search</param>
        public static void ShowProgress (int totalFiles)
        {
            ShowProgress(totalFiles, 0);
        }

        /// <summary>
        /// Prints progress to console - a '0' passed for totalSearched will be ignored and the last positive value
        /// will be used as this is a running talley (i.e. it cannot decrease) 
        /// </summary>
        /// <param name="totalFiles">The total number of files included in the search</param>
        /// <param name="totalSearched">The number of files that have been searched so far</param>
        public static void ShowProgress (int totalFiles, int totalSearched)
        {
            ShowProgress(totalFiles, totalSearched, 0);
        }

        /// <summary>
        /// Prints progress to console - a '0' passed for totalSearched or numHits will be ignored and the last positive value
        /// will be used as this is a running talley (i.e. it cannot decrease) 
        /// </summary>
        /// <param name="totalFiles">The total number of files included in the search</param>
        /// <param name="totalSearched">The number of files that have been searched so far</param>
        /// <param name="numHits">The number of files searched which contain a match for the search param</param>
        public static void ShowProgress (int totalFiles, int totalSearched, int numHits)
        {
            ShowProgress(totalFiles, totalSearched, numHits, 0);
        }

        /// <summary>
        /// Prints progress to console - a '0' passed for totalSearched, numHits, or errors will be ignored and the last positive value
        /// will be used as this is a running talley (i.e. it cannot decrease)  
        /// </summary>
        /// <param name="totalFiles">The total number of files included in the search</param>
        /// <param name="totalSearched">The number of files that have been searched so far</param>
        /// <param name="numHits">The number of files searched which contain a match for the search param</param>
        /// <param name="errors">The number of files which have not been searched due to error</param>
        public static void ShowProgress(int totalFiles, int totalSearched, int numHits, int errors)
        {
            if (totalSearched != 0)
                Display.totalSearched = totalSearched;

            if (numHits != 0)
                Display.numHits = numHits;

            if (errors != 0)
                Display.errors = errors;

            var progressMessage = 
                string.Format("Total Files: {0}  Searched: {1}  Hits: {2}  Errors: {3}",
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

                Console.WriteLine("The following files were unable to be searched and were skipped during the process: ");
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

            Console.Write("\r\n");

            Console.WriteLine("The following files contained the search string: ");
            foreach (string file in foundFiles)
            {
                Console.WriteLine(file);
            }
        }
    }
}
