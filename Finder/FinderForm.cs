using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Finder
{
    public partial class FinderForm : Form
    {
        //private vars
        private Finder finder;
        private int totalFiles;
        private int searchedFiles;

        public FinderForm(string[] args)
        {
            InitializeComponent();
            finder = new Finder(args);
            finder.ProgressReport += ShowProgress;
            FileSearcher.FileFound += OnFileFound;

            finder.InitializeArgs(args);
            SetFormArgs();

            backgroundWorkerFinder.DoWork += new DoWorkEventHandler(backgroundWorkerFinder_DoWork);
        }

        private void SetFormArgs()
        {
            textBoxDirectory.Text = finder.SearchDirectory;
            checkBoxSearchSubdirectories.Checked = finder.IncludeSubfolders;
            checkBoxUseRegex.Checked = finder.UseRegex;
        }

        private void Initialize()
        {
            listBoxResults.Items.Clear();

            totalFiles = 0;
            searchedFiles = 0;

            finder.SearchDirectory = textBoxDirectory.Text;
            finder.SearchString = textBoxSearchString.Text;
            finder.UseRegex = checkBoxUseRegex.Checked;
            finder.IncludeSubfolders = checkBoxSearchSubdirectories.Checked;
        }

        private void AddResult(string fileName)
        {
            if (listBoxResults.InvokeRequired)
            {
                Invoke(new Action<string>(AddResult), new object[] { fileName });
                return;
            }

            listBoxResults.Items.Add(fileName);
        }

        private void ShowProgress(object sender, Milestones milestone)
        {
            switch (milestone)
            {
                case (Milestones.GotFiles):
                    totalFiles = finder.AllFiles.Length;
                    break;
                case (Milestones.SearchedFile):
                    searchedFiles++;
                    double progress =
                        Math.Round(((double)searchedFiles / (double)totalFiles) * 100);
                    UpdateProgressBar(Convert.ToInt32(progress));
                    break;
                case (Milestones.Done):
                    break;
            }
        }

        private void UpdateProgressBar(int progressPercentage)
        {
            if (progressBar.InvokeRequired)
            {
                Invoke(new Action<int>(UpdateProgressBar), new object[] { progressPercentage });
            }
            progressBar.Value = progressPercentage;
        }

        //events
        private void OnFileFound(object sender, string fileName)
        {
            AddResult(fileName);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            Initialize();
            backgroundWorkerFinder.RunWorkerAsync();
        }

        private void buttonFolderBrowser_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxDirectory.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void backgroundWorkerFinder_DoWork(object sender, DoWorkEventArgs e)
        {
            finder.Find();
        }
    }
}

