using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Finder
{
    public class FileSearcher
    {
        public static event EventHandler<string> FileFound;
        public static event EventHandler<string> ErrorFound;

        public void Search(string fileName, string searchString)
        {
            string[] allLines = null;

            try
            {
                allLines = File.ReadAllLines(fileName);
            }
            catch
            {
                OnErrorFound(fileName);
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

        public void SearchRegex(string fileName, string searchString)
        {
            string text = null;
            try
            {
                text = File.ReadAllText(fileName);
            }
            catch
            {
                OnErrorFound(fileName);
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

        protected virtual void OnErrorFound(string fileName)
        {
            ErrorFound?.Invoke(this, fileName);
        }
    }
}
