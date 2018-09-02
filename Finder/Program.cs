using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Finder
{
    class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        [STAThread]
        static void Main(string[] args)
        {
            if (args.ToList().Exists(a => Args.UseGui.ToList().Contains(a)))
            {
                var handle = GetConsoleWindow();
                ShowWindow(handle, SW_HIDE);
                Application.EnableVisualStyles();
                Application.Run(new FinderForm(args));
            }

            else
            {
                if (args.Length < 1)
                {
                    Console.WriteLine("You must pass a search string as an argument, Enter " + Args.Help[1] + " for more information on usage");
                    Environment.Exit(0);
                }

                var finder = new Finder(args);
            }
        }
    }
}
