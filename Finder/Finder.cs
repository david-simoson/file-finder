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
            searcher = new FileSearcher(searchString);
            searcher.FileFound += OnFileFound;
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

            Display.NewLine("Searching " + allFiles.Length + " files");

            //now process the files
            foreach (string file in allFiles)
            {
                if (!useRegex)
                {
                    searcher.Search(file);
                }
                else //if a regex search
                {
                    searcher.SearchRegex(file);
                }
            }

            Display.PrintSummary(allFiles, foundFiles.ToArray());
        }  

        private void InitializeArgs(string[] args)
        {
            searchString = args[0];

            var ignore = false;

            if (args.Length > 1)
            {
                for (int i = 1; i < args.Length; i++)
                {
                    if (ignore)
                    {
                        var dirName = currDir + "\\" + args[i].Replace("\"", "");

                        if (!Directory.Exists(dirName))
                        {
                            Display.NewLine("-ignore arg must be followed by a valid folder to ignore - please use \"-help\" for usage information");
                            Environment.Exit(0);
                        }

                        excludedFolders.Add(dirName.ToLower());

                        ignore = false;
                        continue;
                    }

                    switch (args[i])
                    {
                        case ("-rgx"):
                            useRegex = true;
                            break;
                        case ("-sf"):
                            includeSubfolders = true;
                            break;
                        case ("-ignore"):
                            if (!includeSubfolders)
                                Display.NewLine("incorrect usage of ignore arg - please use \"-help\" for usage information");

                            ignore = true;
                            break;
                        default:
                            Display.NewLine("\"" + args[i] + "\" is not a valid argument - use \"-help\" for usage information on acceptable arguments");
                            Environment.Exit(0);
                            break;
                    }
                }
            }
        }

        private void OnFileFound(object sender, string fileName)
        {
            Display.NumHits++;

            if (!foundFiles.Contains(fileName))
            {
                foundFiles.Add(fileName);
            }

            Display.RewriteLine("Match Found");
        }
    }
}
