using System;
using System.Collections.Generic;
using System.Linq;

namespace Finder
{
    public class FinderConsole
    {
        //private vars
        private Finder finder;
        private int totalFiles;
        private int searchedFiles;
        private List<string> foundFiles;
        private List<string> errorFiles;
        private bool verbose;

        public FinderConsole(string[] args)
        {
            foundFiles = new List<string>();
            errorFiles = new List<string>();
            finder = new Finder(args);
            finder.ProgressReport += OnShowProgress;
            finder.ErrorMessage += OnHandleError;
            FileSearcher.FileFound += OnFileFound;
            FileSearcher.ErrorFound += OnErrorFound;
            finder.InitializeArgs(args);
            verbose = finder.Verbose;
            finder.Find();
        }

        private void OnShowProgress(object sender, Milestones milestone)
        {
            switch (milestone)
            {
                case (Milestones.GotFiles):
                    totalFiles = finder.AllFiles.Length;
                    if (!verbose)
                        ShowProgress();
                    break;
                case (Milestones.SearchedFile):
                    searchedFiles++;
                    var fileName = sender as string;
                    if (!verbose)
                        ShowProgress();
                    else
                        Console.WriteLine("Searched: " + fileName);
                    break;
                case (Milestones.Done):
                    PrintSummary(foundFiles, errorFiles);
                    break;
            }
        }

        private void OnHandleError(object sender, string errorMessage)
        {
            Console.WriteLine(errorMessage);
            Environment.Exit(0);
        }

        private void OnFileFound(object sender, string fileName)
        {
            if (!foundFiles.Contains(fileName))
                foundFiles.Add(fileName);

            if (!verbose)
            {
                ShowProgress();
            }
            else
            {
                Console.WriteLine("MATCH FOUND: " + fileName);
            }
        }

        private void OnErrorFound(object sender, string fileName)
        {
            if (!errorFiles.Contains(fileName))
                errorFiles.Add(fileName);

            if (!verbose)
            {
                 ShowProgress();
            }
            else
            {
                Console.WriteLine("ERROR SEARCHING: " + fileName);
            }

        }

        private void ShowProgress()
        {
            var progressMessage =
                string.Format("Total Files: {0}  Searched: {1}  Hits: {2}  Errors: {3}",
                totalFiles,
                searchedFiles,
                foundFiles.Count(),
                errorFiles.Count());

            Console.Write("\r" + progressMessage);
        }

        private void PrintSummary(List<string> foundFiles, List<string> errorFiles)
        {
            if (verbose)
            {
                Console.WriteLine("");
                ShowProgress();//already present if !verbose
            }

            if (errorFiles.Count() > 0)
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
