namespace Finder
{
    partial class FinderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonFolderBrowser = new System.Windows.Forms.Button();
            this.checkBoxSearchSubdirectories = new System.Windows.Forms.CheckBox();
            this.checkBoxUseRegex = new System.Windows.Forms.CheckBox();
            this.labelResults = new System.Windows.Forms.Label();
            this.labelSearchString = new System.Windows.Forms.Label();
            this.labelDirectory = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.textBoxSearchString = new System.Windows.Forms.TextBox();
            this.listBoxResults = new System.Windows.Forms.ListBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.textBoxDirectory = new System.Windows.Forms.TextBox();
            this.backgroundWorkerFinder = new System.ComponentModel.BackgroundWorker();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // buttonFolderBrowser
            // 
            this.buttonFolderBrowser.Location = new System.Drawing.Point(432, 12);
            this.buttonFolderBrowser.Name = "buttonFolderBrowser";
            this.buttonFolderBrowser.Size = new System.Drawing.Size(75, 27);
            this.buttonFolderBrowser.TabIndex = 21;
            this.buttonFolderBrowser.Text = "Select";
            this.buttonFolderBrowser.UseVisualStyleBackColor = true;
            this.buttonFolderBrowser.Click += new System.EventHandler(this.buttonFolderBrowser_Click);
            // 
            // checkBoxSearchSubdirectories
            // 
            this.checkBoxSearchSubdirectories.AutoSize = true;
            this.checkBoxSearchSubdirectories.Location = new System.Drawing.Point(243, 78);
            this.checkBoxSearchSubdirectories.Name = "checkBoxSearchSubdirectories";
            this.checkBoxSearchSubdirectories.Size = new System.Drawing.Size(192, 24);
            this.checkBoxSearchSubdirectories.TabIndex = 20;
            this.checkBoxSearchSubdirectories.Text = "Search Subdirectories";
            this.checkBoxSearchSubdirectories.UseVisualStyleBackColor = true;
            // 
            // checkBoxUseRegex
            // 
            this.checkBoxUseRegex.AutoSize = true;
            this.checkBoxUseRegex.Location = new System.Drawing.Point(140, 78);
            this.checkBoxUseRegex.Name = "checkBoxUseRegex";
            this.checkBoxUseRegex.Size = new System.Drawing.Size(81, 24);
            this.checkBoxUseRegex.TabIndex = 19;
            this.checkBoxUseRegex.Text = "Regex";
            this.checkBoxUseRegex.UseVisualStyleBackColor = true;
            // 
            // labelResults
            // 
            this.labelResults.AutoSize = true;
            this.labelResults.Location = new System.Drawing.Point(31, 164);
            this.labelResults.Name = "labelResults";
            this.labelResults.Size = new System.Drawing.Size(67, 20);
            this.labelResults.TabIndex = 18;
            this.labelResults.Text = "Results:";
            // 
            // labelSearchString
            // 
            this.labelSearchString.AutoSize = true;
            this.labelSearchString.Location = new System.Drawing.Point(28, 48);
            this.labelSearchString.Name = "labelSearchString";
            this.labelSearchString.Size = new System.Drawing.Size(110, 20);
            this.labelSearchString.TabIndex = 17;
            this.labelSearchString.Text = "Search String:";
            // 
            // labelDirectory
            // 
            this.labelDirectory.AutoSize = true;
            this.labelDirectory.Location = new System.Drawing.Point(28, 15);
            this.labelDirectory.Name = "labelDirectory";
            this.labelDirectory.Size = new System.Drawing.Size(76, 20);
            this.labelDirectory.TabIndex = 16;
            this.labelDirectory.Text = "Directory:";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(140, 148);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(306, 23);
            this.progressBar.TabIndex = 15;
            // 
            // textBoxSearchString
            // 
            this.textBoxSearchString.Location = new System.Drawing.Point(149, 45);
            this.textBoxSearchString.Name = "textBoxSearchString";
            this.textBoxSearchString.Size = new System.Drawing.Size(358, 26);
            this.textBoxSearchString.TabIndex = 12;
            // 
            // listBoxResults
            // 
            this.listBoxResults.FormattingEnabled = true;
            this.listBoxResults.ItemHeight = 20;
            this.listBoxResults.Location = new System.Drawing.Point(81, 196);
            this.listBoxResults.Name = "listBoxResults";
            this.listBoxResults.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxResults.Size = new System.Drawing.Size(401, 364);
            this.listBoxResults.TabIndex = 14;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(140, 108);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(75, 34);
            this.buttonSearch.TabIndex = 13;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // textBoxDirectory
            // 
            this.textBoxDirectory.Location = new System.Drawing.Point(149, 12);
            this.textBoxDirectory.Name = "textBoxDirectory";
            this.textBoxDirectory.Size = new System.Drawing.Size(272, 26);
            this.textBoxDirectory.TabIndex = 11;
            // 
            // FinderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 588);
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

        #endregion

        private System.Windows.Forms.Button buttonFolderBrowser;
        private System.Windows.Forms.CheckBox checkBoxSearchSubdirectories;
        private System.Windows.Forms.CheckBox checkBoxUseRegex;
        private System.Windows.Forms.Label labelResults;
        private System.Windows.Forms.Label labelSearchString;
        private System.Windows.Forms.Label labelDirectory;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.TextBox textBoxSearchString;
        private System.Windows.Forms.ListBox listBoxResults;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.TextBox textBoxDirectory;
        private System.ComponentModel.BackgroundWorker backgroundWorkerFinder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}