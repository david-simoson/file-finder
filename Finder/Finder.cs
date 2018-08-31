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

            Display.ShowProgress(allFiles.Length);

            //now process the files
            for (int i = 0; i < allFiles.Length; i++)
            {
                if (!useRegex)
                {
                    searcher.Search(allFiles[i]);
                }
                else //if a regex search
                {
                    searcher.SearchRegex(allFiles[i]);
                }
                Display.ShowProgress(allFiles.Length, i + 1);
            }

            Display.PrintSummary(foundFiles.ToArray(), errorFiles.ToArray());
        }  

        private void InitializeArgs(string[] args)
        {
            searchString = args[0];

            var ignore = false;
            
            for (int i = 0; i < args.Length; i++)
            {
                if (ignore)
                {
                    var dirName = currDir + "\\" + args[i].Replace("\"", "");

                    if (!Directory.Exists(dirName))
                    {
                        Console.WriteLine(Args.IgnoreDirectory + " must be followed by a valid folder to ignore - please use " + Args.Help + " for usage information");
                        Environment.Exit(0);
                    }

                    excludedFolders.Add(dirName.ToLower());

                    ignore = false;
                    continue;
                }

                switch (args[i])
                {
                    case (Args.Help):
                        Console.WriteLine(Args.HelpDocumentation);
                        Environment.Exit(0);
                        break;
                    case (Args.UseRegex):
                        useRegex = true;
                        break;
                    case (Args.IncludeSubDirectories):
                        includeSubfolders = true;
                        break;
                    case (Args.IgnoreDirectory):
                        if (!includeSubfolders)
                            Console.WriteLine("incorrect usage of ignore arg - please use " + Args.Help + " for usage information");

                        ignore = true;
                        break;
                    default:
                        //Console.WriteLine("\"" + args[i] + "\" is not a valid argument - use " + Args.Help + " for usage information on acceptable arguments");
                        //Environment.Exit(0);
                        break;
                }
            }
        }

        private void OnFileFound(object sender, string fileName)
        {
            if (!foundFiles.Contains(fileName))
            {
                foundFiles.Add(fileName);
            }

            Display.ShowProgress(allFiles.Length, 0, foundFiles.Count(), 0);
        }

        private void OnErrorFound(object sender, string fileName)
        {
            if (!errorFiles.Contains(fileName))
            {
                errorFiles.Add(fileName);
            }

            Display.ShowProgress(allFiles.Length, 0, 0, errorFiles.Count());
        }
    }
}
