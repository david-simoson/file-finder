using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Finder(string searchString)
        {
            currDir = Directory.GetCurrentDirectory();
            this.searchString = searchString;
            foundFiles = new List<string>();
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

                    totalFound++;

                    if (!foundFiles.Contains(file))
                        foundFiles.Add(file);

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

        protected virtual void OnFindingEvent(FindingEventArgs args)
        {
            FindingEvent?.Invoke(this, args);
        }
    }
}
