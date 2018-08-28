﻿using System.IO;
using System.Collections.Generic;

namespace Finder
{
    class Finder
    {
        //private vars
        private string currDir;
        private string[] allFiles;
        private string searchString;
        private List<string> foundFiles;

        //args
        private bool useRegex = false;

        public Finder(string[] args)
        {
            if (args.Length > 0)
                InitializeArgs(args);

            currDir = Directory.GetCurrentDirectory();
            foundFiles = new List<string>();
            Find();
        }

        public void Find()
        {
            allFiles = Directory.GetFiles(currDir);

            Display.NewLine("Searching " + allFiles.Length + " files");

            //now process the files
            foreach (string file in allFiles)
            {
                using (FileSearcher searcher = new FileSearcher(file, searchString))
                {
                    searcher.FileFound += OnFileFound;

                    if (!useRegex)
                    {
                        searcher.Search();

                    }
                    else //if a regex search
                    {
                        searcher.SearchRegex();
                    }
                }
            }

            Display.PrintSummary(allFiles, foundFiles.ToArray());
        }

        private void InitializeArgs(string[] args)
        {
            searchString = args[0];

            foreach (string arg in args)
            {
                switch (arg)
                {
                    case ("-rgx"):
                        useRegex = true;
                        break;
                    default:
                        //TODO: Notify user that their argument is not handled
                        break;
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
