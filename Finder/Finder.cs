using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Finder
{
    public enum Milestones
    {
        GotFiles,
        SearchedFile,
        Done
    }

    public class Finder
    {
        //events
        public event EventHandler<string> ErrorMessage;
        public event EventHandler<Milestones> ProgressReport;

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
        public string[] AllFiles { get => allFiles; set => allFiles = value; }
        public List<string> FoundFiles { get => foundFiles; set => foundFiles = value; }
        public List<string> ErrorFiles { get => errorFiles; set => errorFiles = value; }
        public string SearchDirectory { get => searchDirectory; set => searchDirectory = value; }
        public string SearchString { get => searchString; set => searchString = value; }
        public bool UseGui { get => useGui; set => useGui = value; }
        public bool UseRegex { get => useRegex; set => useRegex = value; }
        public bool IncludeSubfolders { get => includeSubfolders; set => includeSubfolders = value; }
        public bool Verbose { get => verbose; set => verbose = value; }

        public Finder(string[] args)
        {
            excludedFolders = new List<string>();

            if (!useGui && string.IsNullOrEmpty(searchDirectory))
                searchDirectory = Directory.GetCurrentDirectory();

            FoundFiles = new List<string>();
            ErrorFiles = new List<string>();
            searcher = new FileSearcher();
            FileSearcher.FileFound += OnFileFound;
            FileSearcher.ErrorFound += OnErrorFound;
        }

        public void Find()
        {
            if (ErrorFiles.Count > 0)
                ErrorFiles = new List<string>();
            if (FoundFiles.Count > 0)
                FoundFiles = new List<string>();

            if (!includeSubfolders)
                allFiles = Directory.GetFiles(searchDirectory);

            else
                allFiles = Directory.GetFiles(searchDirectory, "*.*", SearchOption.AllDirectories)
                    .Where(f => !excludedFolders.Contains(Path.GetDirectoryName(f).ToLower()))
                    .ToArray();

            ProgressReport(this, Milestones.GotFiles);

            for (var i = 0; i < allFiles.Length; i++)
            {
                if (!useRegex)
                {
                    searcher.Search(allFiles[i], searchString);
                }
                else
                {
                    searcher.SearchRegex(allFiles[i], searchString);
                }

                //TODO: fix this - ugly
                double progress =
                    Math.Round(((double)i / (double)allFiles.Length) * 100);

                ProgressReport(allFiles[i], Milestones.SearchedFile);
            }

            ProgressReport(this, Milestones.Done);
        }

        public void InitializeArgs(string[] args)
        {
            if (args.ToList().Exists(a => Args.SetDirectory.ToList().Contains(a)))
            {
                var argIndex = args.ToList().FindIndex(a => Args.SetDirectory.ToList().Contains(a));

                searchDirectory = args[argIndex + 1].Replace("\"", "");

                if (!Directory.Exists(searchDirectory))
                    OnErrorMessage(this, searchDirectory + " not found");

                var tmpArgs = args.ToList();
                tmpArgs.RemoveRange(argIndex, 2);

                args = tmpArgs.ToArray();
            }

            searchString = args[0];

            var ignore = false;

            for (var i = 0; i < args.Length; i++)
            {
                if (ignore)
                {
                    if (!includeSubfolders)
                        includeSubfolders = true;//this argument is implied when ignore is used

                    var dirName = searchDirectory + "\\" + args[i].Replace("\"", "");

                    if (!Directory.Exists(dirName))
                    {
                        if (!args[i].StartsWith("-"))//indicated a directory but it is not found
                            OnErrorMessage(this, searchDirectory + " not found");

                        if (excludedFolders.Count() == 0)
                        {
                            OnErrorMessage(this, Args.IgnoreDirectory[1] + " used but no folders excluded");
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
                else if (Args.UseGui.Contains(args[i]))
                    useGui = true;
                else if (Args.Verbose.Contains(args[i]))
                    verbose = true;
                else if (Args.IncludeSubDirectories.Contains(args[i]))
                    includeSubfolders = true;
                else if (Args.IgnoreDirectory.Contains(args[i]))
                {
                    if (i == args.Length - 1)
                    {
                        OnErrorMessage(this, Args.IgnoreDirectory[1] + " was used incorrectly as the last argument (no directory was passed to ignore)");
                    }

                    ignore = true;
                }
                else
                {
                    if (i != 0)
                    {
                        OnErrorMessage(this, args[i] + " is not a valid argument");
                    }
                }
            }
        }

        private void OnFileFound(object sender, string fileName)
        {
            if (!foundFiles.Contains(fileName))
            {
                foundFiles.Add(fileName);
            }
        }

        private void OnErrorFound(object sender, string fileName)
        {
            if (!errorFiles.Contains(fileName))
            {
                errorFiles.Add(fileName);
            }
        }

        protected virtual void OnErrorMessage(object sender, string errorMessage)
        {
            ErrorMessage?.Invoke(sender, errorMessage);
        }

        protected virtual void OnProgressReport(object sender, Milestones milestone)
        {
            ProgressReport?.Invoke(sender, milestone);
        }




    }
}