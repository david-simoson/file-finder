using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Finder
{
    public class FinderForm : Form
    {
        //controls
        private FolderBrowserDialog folderBrowserDialog;
        private TextBox textBoxDirectory;
        private Button buttonSearch;
        private ListBox listBoxResults;
        private TextBox textBoxSearchString;
        private BackgroundWorker backgroundWorkerFinder;
        private ProgressBar progressBar;
        private Label labelDirectory;
        private Label labelSearchString;
        private Label labelResults;
        private CheckBox checkBoxUseRegex;
        private CheckBox checkBoxSearchSubdirectories;
        private Button buttonFolderBrowser;

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

        private void InitializeComponent()
        {
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.textBoxDirectory = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.listBoxResults = new System.Windows.Forms.ListBox();
            this.textBoxSearchString = new System.Windows.Forms.TextBox();
            this.backgroundWorkerFinder = new System.ComponentModel.BackgroundWorker();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.labelDirectory = new System.Windows.Forms.Label();
            this.labelSearchString = new System.Windows.Forms.Label();
            this.labelResults = new System.Windows.Forms.Label();
            this.checkBoxUseRegex = new System.Windows.Forms.CheckBox();
            this.checkBoxSearchSubdirectories = new System.Windows.Forms.CheckBox();
            this.buttonFolderBrowser = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxDirectory
            // 
            this.textBoxDirectory.Location = new System.Drawing.Point(120, 22);
            this.textBoxDirectory.Name = "textBoxDirectory";
            this.textBoxDirectory.Size = new System.Drawing.Size(272, 26);
            this.textBoxDirectory.TabIndex = 0;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(120, 118);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(75, 34);
            this.buttonSearch.TabIndex = 2;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // listBoxResults
            // 
            this.listBoxResults.FormattingEnabled = true;
            this.listBoxResults.ItemHeight = 20;
            this.listBoxResults.Location = new System.Drawing.Point(61, 206);
            this.listBoxResults.Name = "listBoxResults";
            this.listBoxResults.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxResults.Size = new System.Drawing.Size(401, 364);
            this.listBoxResults.TabIndex = 3;
            // 
            // textBoxSearchString
            // 
            this.textBoxSearchString.Location = new System.Drawing.Point(120, 55);
            this.textBoxSearchString.Name = "textBoxSearchString";
            this.textBoxSearchString.Size = new System.Drawing.Size(358, 26);
            this.textBoxSearchString.TabIndex = 1;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(120, 158);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(306, 23);
            this.progressBar.TabIndex = 4;
            // 
            // labelDirectory
            // 
            this.labelDirectory.AutoSize = true;
            this.labelDirectory.Location = new System.Drawing.Point(8, 25);
            this.labelDirectory.Name = "labelDirectory";
            this.labelDirectory.Size = new System.Drawing.Size(76, 20);
            this.labelDirectory.TabIndex = 5;
            this.labelDirectory.Text = "Directory:";
            // 
            // labelSearchString
            // 
            this.labelSearchString.AutoSize = true;
            this.labelSearchString.Location = new System.Drawing.Point(8, 58);
            this.labelSearchString.Name = "labelSearchString";
            this.labelSearchString.Size = new System.Drawing.Size(110, 20);
            this.labelSearchString.TabIndex = 6;
            this.labelSearchString.Text = "Search String:";
            // 
            // labelResults
            // 
            this.labelResults.AutoSize = true;
            this.labelResults.Location = new System.Drawing.Point(11, 174);
            this.labelResults.Name = "labelResults";
            this.labelResults.Size = new System.Drawing.Size(67, 20);
            this.labelResults.TabIndex = 7;
            this.labelResults.Text = "Results:";
            // 
            // checkBoxUseRegex
            // 
            this.checkBoxUseRegex.AutoSize = true;
            this.checkBoxUseRegex.Location = new System.Drawing.Point(120, 88);
            this.checkBoxUseRegex.Name = "checkBoxUseRegex";
            this.checkBoxUseRegex.Size = new System.Drawing.Size(81, 24);
            this.checkBoxUseRegex.TabIndex = 8;
            this.checkBoxUseRegex.Text = "Regex";
            this.checkBoxUseRegex.UseVisualStyleBackColor = true;
            // 
            // checkBoxSearchSubdirectories
            // 
            this.checkBoxSearchSubdirectories.AutoSize = true;
            this.checkBoxSearchSubdirectories.Location = new System.Drawing.Point(223, 88);
            this.checkBoxSearchSubdirectories.Name = "checkBoxSearchSubdirectories";
            this.checkBoxSearchSubdirectories.Size = new System.Drawing.Size(192, 24);
            this.checkBoxSearchSubdirectories.TabIndex = 9;
            this.checkBoxSearchSubdirectories.Text = "Search Subdirectories";
            this.checkBoxSearchSubdirectories.UseVisualStyleBackColor = true;
            // 
            // buttonFolderBrowser
            // 
            this.buttonFolderBrowser.Location = new System.Drawing.Point(403, 22);
            this.buttonFolderBrowser.Name = "buttonFolderBrowser";
            this.buttonFolderBrowser.Size = new System.Drawing.Size(75, 23);
            this.buttonFolderBrowser.TabIndex = 10;
            this.buttonFolderBrowser.Text = "Select";
            this.buttonFolderBrowser.UseVisualStyleBackColor = true;
            this.buttonFolderBrowser.Click += new System.EventHandler(this.buttonFolderBrowser_Click);
            // 
            // FinderForm
            // 
            this.ClientSize = new System.Drawing.Size(507, 600);
            this.Controls.Add(this.buttonFolderBrowser);
            this.Controls.Add(this.checkBoxSearchSubdirectories);
            this.Controls.Add(this.checkBoxUseRegex);
            this.Controls.Add(this.labelResults);
            this.Controls.Add(this.labelSearchString);
            this.Controls.Add(this.labelDirectory);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.textBoxSearchString);
            this.Controls.Add(this.listBoxResults);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.textBoxDirectory);
            this.Name = "FinderForm";
            this.Text = "Finder";
            this.ResumeLayout(false);
            this.PerformLayout();

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

        private void ChooseFolder()
        {
        }

        private void backgroundWorkerFinder_DoWork(object sender, DoWorkEventArgs e)
        {
            finder.Find();
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
    }
}
