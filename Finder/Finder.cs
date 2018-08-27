using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Finder
{
    class Finder
    {
        public IEnumerable<string> FoundFiles { get { return foundFiles; } }
        public string[] AllFiles { get { return allFiles; } }

        public event EventHandler<FindingEventArgs> FindingEvent;

        private string currDir;
        private string[] allFiles;
        private string searchString;
        private int totalFound;
        private List<string> foundFiles;

        //args
        private bool useRegex = false;

        public Finder(string searchString, bool useRegex = false)
        {
            currDir = Directory.GetCurrentDirectory();
            this.searchString = searchString;
            foundFiles = new List<string>();

            //args
            this.useRegex = useRegex;

        }

        public void Find()
        {
            allFiles = Directory.GetFiles(currDir);

            var displayText = "Searching " + allFiles.Length + " files...";

            OnFindingEvent(new FindingEventArgs
            {
                EventType = FindingEventTypes.Header,
                Display = displayText
            });

            //now process the files
            foreach (string file in allFiles)
            {
                SearchFile(file);
            }

            OnFindingEvent(new FindingEventArgs
            {
                EventType = FindingEventTypes.Summary
            });
        }

        private void SearchFile(string file)
        {
            if (!useRegex)
            {
                var allLines = File.ReadAllLines(file);
                OnFindingEvent(new FindingEventArgs
                {
                    EventType = FindingEventTypes.Progress,
                    FileName = file
                });


                Parallel.ForEach(allLines, new Action<string, ParallelLoopState>((string line, ParallelLoopState state) =>
                {
                    if (line.Contains(searchString))
                    {
                        if (!foundFiles.Contains(file))
                        {
                            totalFound++;
                            foundFiles.Add(file);
                        }

                        OnFindingEvent(new FindingEventArgs
                        {
                            EventType = FindingEventTypes.Found,
                            TotalFound = totalFound,
                            FileName = file
                        });

                        state.Break();
                    }
                }));
            }
            else
            {
                var text = File.ReadAllText(file);
                OnFindingEvent(new FindingEventArgs
                {
                    EventType = FindingEventTypes.Progress,
                    FileName = file
                });

                if (searchString.StartsWith("/"))
                    searchString = searchString.Remove(0, 1);

                if (searchString.EndsWith("/"))
                    searchString = searchString.Remove(searchString.Length - 1, 1);

                Regex rx = new Regex(searchString);

                if (rx.IsMatch(text))
                {
                    totalFound++;

                    if (!foundFiles.Contains(file))
                        foundFiles.Add(file);

                    OnFindingEvent(new FindingEventArgs
                    {
                        EventType = FindingEventTypes.Found,
                        TotalFound = totalFound,
                        FileName = file
                    });
                }
            }
        }

        protected virtual void OnFindingEvent(FindingEventArgs args)
        {
            FindingEvent?.Invoke(this, args);
        }
    }
}
