using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finder
{
    static class Display
    {
        public static int NumHits { get; set; }

        public static string FileName { get; set; }

        private static int lastMessageLength;

        private static bool needCarriageReturn = false;

        public static void NewLine(string message)
        {
            StringBuilder sb = new StringBuilder();

            if (needCarriageReturn)
                sb.Append("\n");

            sb.Append(message);

            Console.WriteLine(sb.ToString());
        }


        public static void RewriteLine(string message)
        {
            needCarriageReturn = true;

            var displayMessage = "Hits: "
                + NumHits
                + "    " + message + ": "
                + FileName;

            if (displayMessage.Length < lastMessageLength)
            {
                displayMessage = PadDisplayMessage(displayMessage);
            }

            lastMessageLength = displayMessage.Length;

            Console.Write("\r{0}", displayMessage);
        }

        public static void PrintSummary(string[] allFiles, string[] foundFiles)
        {
            StringBuilder sb = new StringBuilder();

            if (needCarriageReturn)
                sb.Append("\n");

            sb.Append("Searched: ")
                .Append(allFiles.Length)
                .Append(" files");

            Console.WriteLine(sb.ToString());

            if (foundFiles.Count() == 0)
            {
                Console.WriteLine("No files were found containing the search string");
                return;
            }

            Console.WriteLine("Found: " + foundFiles.Count() + " files containing the search string: ");
            foreach (string file in foundFiles)
            {
                Console.WriteLine(file);
            }
        }


        private static string PadDisplayMessage(string message)
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
