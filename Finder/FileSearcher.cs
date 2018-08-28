using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Finder
{
    public class FileSearcher : IDisposable
    {
        public event EventHandler<string> FileFound;

        private string fileName;
        private string searchString;
        private bool disposed = false;
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public FileSearcher(string fileName, string searchString)
        {
            this.fileName = fileName;
            this.searchString = searchString;

            Display.FileName = fileName;
            Display.RewriteLine("Processing file");

        }

        public void Search()
        {
            string[] allLines = null;

            try
            {
                allLines = File.ReadAllLines(fileName);
            }
            catch
            {
                //TODO: log this?
            }

            if (allLines == null)
            {
                //TODO: add notification to user that this file was not searched - but continue searching

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

        public void SearchRegex()
        {
            string text = null;
            try
            {
                text = File.ReadAllText(fileName);
            }
            catch
            {
                //TODO: log this?
            }

            if (String.IsNullOrEmpty(text))
            {
                //TODO: add notification to user that this file was not searched - but continue searching

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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            disposed = true;
        }
    }
}
