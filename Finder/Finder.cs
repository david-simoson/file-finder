using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Finder
{
    public class Finder
    {
        public event EventHandler<int> FileSearched;

        //private vars
        private string searchDirectory;
        private string[] allFiles;
        private string searchString;
        private List<string> foundFiles;
        private List<string> errorFiles;
        private FileSearcher searcher;

        //args
        private bool useGui = false;
        private bool useRegex = false;
        private bool includeSubfolders = false;
        private bool verbose = false;
        private List<string> excludedFolders;

        //properties
        public string SearchDirectory { get => searchDirectory; set => searchDirectory = value; }
        public string SearchString { get => searchString; set => searchString = value; }
        public bool UseGui { get => useGui; set => useGui = value; }
        public bool UseRegex { get => useRegex; set => useRegex = value; }
        public bool IncludeSubfolders { get => includeSubfolders; set => includeSubfolders = value; }
        public bool Verbose { get => verbose; set => verbose = value; }

        public Finder(string[] args)
        {
            searchDirectory = Directory.GetCurrentDirectory();
            excludedFolders = new List<string>();

            if (args.Length > 0)
                InitializeArgs(args);

            foundFiles = new List<string>();
            errorFiles = new List<string>();
            searcher = new FileSearcher();
            FileSearcher.FileFound += OnFileFound;
            FileSearcher.ErrorFound += OnErrorFound;

            if (!UseGui)
                Find();
        }

        public void Find()
        {
            if (errorFiles.Count > 0)
                errorFiles = new List<string>();
            if (foundFiles.Count > 0)
                foundFiles = new List<string>();

            if (!IncludeSubfolders)
                allFiles = Directory.GetFiles(searchDirectory);

            else
                allFiles = Directory.GetFiles(searchDirectory, "*.*", SearchOption.AllDirectories)
                    .Where(f => !excludedFolders.Contains(Path.GetDirectoryName(f).ToLower()))
                    .ToArray();

            if (!Verbose)
            {
                if (!useGui)
                    Display.ShowProgress(allFiles.Length);
            }

            for (var i = 0; i < allFiles.Length; i++)
            {
                if (!UseRegex)
                {
                    searcher.Search(allFiles[i], searchString);
                }
                else
                {
                    searcher.SearchRegex(allFiles[i], searchString);
                }
                if (!Verbose)
                {
                    if (!useGui)
                        Display.ShowProgress(allFiles.Length, i + 1);
                }
                else
                {
                    if (!useGui)
                        Console.WriteLine("Searched: " + allFiles[i]);
                }
                
                //TODO: fix this - ugly
                double progress = 
                    Math.Round(((double)i / (double)allFiles.Length) * 100);

                OnFileSearched(this, Convert.ToInt32(progress));
            }

            if (!useGui)
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
                    var dirName = searchDirectory + "\\" + args[i].Replace("\"", "");

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
                    UseRegex = true;
                else if (Args.UseGui.Contains(args[i]))
                    UseGui = true;
                else if (Args.Verbose.Contains(args[i]))
                    Verbose = true;
                else if (Args.IncludeSubDirectories.Contains(args[i]))
                    IncludeSubfolders = true;
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

            if (excludedFolders.Count() > 0 && !IncludeSubfolders)
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

            if (!Verbose)
            {
                if (!useGui)
                    Display.ShowProgress(allFiles.Length, 0, foundFiles.Count());
            }
            else
            {
                if (!useGui)
                    Console.WriteLine("MATCH FOUND: " + fileName);
            }
        }

        private void OnErrorFound(object sender, string fileName)
        {
            if (!errorFiles.Contains(fileName))
            {
                errorFiles.Add(fileName);
            }

            if (!Verbose)
            {
                if (!useGui)
                    Display.ShowProgress(allFiles.Length, 0, 0, errorFiles.Count());
            }
            else
            {
                if (!useGui)
                    Console.WriteLine("ERROR SEARCHING: " + fileName);
            }
        }

        protected virtual void OnFileSearched(object sender, int progressPercentage)
        {
            FileSearched?.Invoke(this, progressPercentage);
        }
    }
}
