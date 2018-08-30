using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Finder
{
    public class FileSearcher
    {
        public event EventHandler<string> FileFound;

        private string searchString;

        public FileSearcher(string searchString)
        {
            this.searchString = searchString;
        }

        public void Search(string fileName)
        {
            Display.RewriteLine("Processing file", fileName);

            string[] allLines = null;

            try
            {
                allLines = File.ReadAllLines(fileName);
            }
            catch
            {
                Display.NewLine("***ERROR SEARCHING: " + fileName + " skipping file and continuing search...");
                return;
            }

            Parallel.ForEach(allLines, new Action<string, ParallelLoopState>((string line, ParallelLoopState state) =>
            {
                if (line.Contains(searchString))
                {
                    OnFileFound(fileName);

                    state.Break();
                }
            }));
        }

        public void SearchRegex(string fileName)
        {
            Display.RewriteLine("Processing file", fileName);

            string text = null;
            try
            {
                text = File.ReadAllText(fileName);
            }
            catch
            {
                Display.NewLine("***ERROR SEARCHING: " + fileName + " skipping file and continuing search...");
                return;
            }

            if (searchString.StartsWith("/"))
                searchString = searchString.Remove(0, 1);

            if (searchString.EndsWith("/"))
                searchString = searchString.Remove(searchString.Length - 1, 1);

            Regex rx = new Regex(searchString);

            if (rx.IsMatch(text))
            {
                OnFileFound(fileName);
            }
        }

        protected virtual void OnFileFound(string fileName)
        {
            FileFound?.Invoke(this, fileName);
        }
    }
}
