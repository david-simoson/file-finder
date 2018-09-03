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
        const int SW_SHOW = 1;

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
                FinderConsole console = new FinderConsole(args);
            }
        }
    }
}
