using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finder
{
    public enum FindingEventTypes
    {
        Header,
        Progress,
        Found,
        Summary
    }

    class Display
    {
        private Finder finder;
        private int lastMessageLength;

        public Display(Finder finder)
        {
            finder.FindingEvent += UpdateDisplay;
            this.finder = finder;
        }

        protected void UpdateDisplay(object sender, FindingEventArgs args)
        {
            switch (args.EventType)
            {
                case (FindingEventTypes.Header):
                    Console.WriteLine(args.Display);
                    break;
                case (FindingEventTypes.Summary):
                    Console.WriteLine("\nSearched: " + finder.AllFiles.Length + " files");

                    if (finder.FoundFiles.Count() == 0)
                    {
                        Console.WriteLine("No files were found containing the search string");
                        break;
                    }

                    Console.WriteLine("Found: " + finder.FoundFiles.Count() + " files containing the search string: ");
                    foreach (string file in finder.FoundFiles)
                    {
                        Console.WriteLine(file);
                    }
                    break;
                case (FindingEventTypes.Found):
                    var message = "Hits: "
                        + args.TotalFound
                        + "   SEARCH MATCH: "
                        + args.FileName;


                    Console.Write("\r{0}", CreateDisplayMessage(message));

                    lastMessageLength = message.Length;

                    break;
                case (FindingEventTypes.Progress):
                    message = "Hits: "
                        + args.TotalFound
                        + "   Processing File: "
                        + args.FileName;

                    Console.Write("\r{0}", CreateDisplayMessage(message));

                    lastMessageLength = message.Length;

                    break;
                default:
                    break;
            }
        }

        private string CreateDisplayMessage(string message)
        {
            StringBuilder padding = new StringBuilder();

            for (int i = message.Length; i <= lastMessageLength; i++)
            {
                padding.Append(" ");
            }

            return message + padding.ToString();
        }
    }
}
