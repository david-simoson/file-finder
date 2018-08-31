using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Finder
{
    class Finder
    {
        //private vars
        private string currDir;
        private string[] allFiles;
        private string searchString;
        private List<string> foundFiles;
        private List<string> errorFiles;
        private FileSearcher searcher;

        //args
        private bool useRegex = false;
        private bool includeSubfolders = false;
        private bool verbose = false;
        private List<string> excludedFolders;

        public Finder(string[] args)
        {
            currDir = Directory.GetCurrentDirectory();
            excludedFolders = new List<string>();

            if (args.Length > 0)
                InitializeArgs(args);

            foundFiles = new List<string>();
            errorFiles = new List<string>();
            searcher = new FileSearcher(searchString);
            searcher.FileFound += OnFileFound;
            searcher.ErrorFound += OnErrorFound;
            Find();
        }

        public void Find()
        {
            if (!includeSubfolders)
                allFiles = Directory.GetFiles(currDir);

            else
                allFiles = Directory.GetFiles(currDir, "*.*", SearchOption.AllDirectories)
                    .Where(f => !excludedFolders.Contains(Path.GetDirectoryName(f).ToLower()))
                    .ToArray();

            if (!verbose)
                Display.ShowProgress(allFiles.Length);

            for (var i = 0; i < allFiles.Length; i++)
            {
                if (!useRegex)
                {
                    searcher.Search(allFiles[i]);
                }
                else
                {
                    searcher.SearchRegex(allFiles[i]);
                }
                if (!verbose)
                    Display.ShowProgress(allFiles.Length, i + 1);
                else
                    Console.WriteLine("Searched: " + allFiles[i]);
            }

            Display.PrintSummary(foundFiles.ToArray(), errorFiles.ToArray());
        }  

        private void InitializeArgs(string[] args)
        {
            searchString = args[0];

            var ignore = false;
            
            for (var i = 0; i < args.Length; i++)
            {
                if (ignore)
                {
                    var dirName = currDir + "\\" + args[i].Replace("\"", "");

                    if (!Directory.Exists(dirName))
                    {
                        if (excludedFolders.Count() == 0)
                        {
                            Console.WriteLine(Args.IgnoreDirectory + " must be followed by valid folder(s) to ignore - please use " + Args.Help[1] + " for usage information");
                            Environment.Exit(0);
                        }
                        ignore = false;
                    }
                    else
                    {
                        excludedFolders.Add(dirName.ToLower());
                        continue;
                    }
                }


                if (Args.Help.Contains(args[i]))
                {
                    Console.WriteLine(Args.HelpDocumentation);
                    Environment.Exit(0);
                }
                else if (Args.UseRegex.Contains(args[i]))
                    useRegex = true;
                else if (Args.Verbose.Contains(args[i]))
                    verbose = true;
                else if (Args.IncludeSubDirectories.Contains(args[i]))
                    includeSubfolders = true;
                else if (Args.IgnoreDirectory.Contains(args[i]))
                {
                    if (i == args.Length - 1)
                    {
                        Console.WriteLine("incorrect usage of " + args[i] + " - please use " + Args.Help[1] + " for usage information");
                        Environment.Exit(0);
                    }

                    ignore = true;
                }
                else
                {
                    if (i != 0)
                    {
                        Console.WriteLine(args[i] + "is not a valid argument, please use " + Args.Help[1] + " for usage information");
                        Environment.Exit(0);
                    }
                }
            }

            if (excludedFolders.Count() > 0 && !includeSubfolders)
            {
                Console.WriteLine("incorrect usage of " + Args.IgnoreDirectory[1] + " - please use " + Args.Help[1] + " for usage information");
                Environment.Exit(0);
            }
        }

        private void OnFileFound(object sender, string fileName)
        {
            if (!foundFiles.Contains(fileName))
            {
                foundFiles.Add(fileName);
            }

            if (!verbose)
                Display.ShowProgress(allFiles.Length, 0, foundFiles.Count());
            else
                Console.WriteLine("MATCH FOUND: " + fileName);
        }

        private void OnErrorFound(object sender, string fileName)
        {
            if (!errorFiles.Contains(fileName))
            {
                errorFiles.Add(fileName);
            }

            if(!verbose)
                Display.ShowProgress(allFiles.Length, 0, 0, errorFiles.Count());
            else
                Console.WriteLine("ERROR SEARCHING: " + fileName);
        }
    }
}
