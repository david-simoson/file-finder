namespace Finder
{
    public static class Args
    {
        private static int padLength = 23;

        public static readonly string[] Help = { "-h", "--help" };

        public static readonly string[] UseRegex = { "-r", "--regex" };

        public static readonly string[] IncludeSubDirectories = { "-s", "--subdirectories" };

        public static readonly string[] IgnoreDirectory = { "-i", "--ignoredirectories" };

        public static readonly string[] Verbose = { "-v", "--verbose" };

        public static readonly string[] UseGui = { "-g", "--usegui" };

        public static readonly string[] SetDirectory = { "-d", "--setdirectory" };

        //Help Strings
        public static readonly string UseRegexHelp = 
            string.Format("{0} indicates that the search string is a regex string", 
                GetPaddedHelpString(UseRegex));

        public static readonly string IncludeSubDirectoriesHelp = 
            string.Format("{0} search will include subdirectories", 
                GetPaddedHelpString(IncludeSubDirectories));

        public static readonly string VerboseHelp =
            string.Format("{0} indicates that console will write one line for each file being searched", 
                GetPaddedHelpString(Verbose));

        public static readonly string UseGuiHelp =
            string.Format("{0} indicates that the gui version will be run instead of the console version",
                GetPaddedHelpString(UseGui));

        public static readonly string SetDirectoryHelp =
            string.Format("{0} indicates the directory to search (e.g)finder [searchstring] {1} [directory]",
                GetPaddedHelpString(SetDirectory), SetDirectory);


        public static readonly string IgnoreDirectoryHelp = string.Format(
@"{0} ignore the contents of a specific subfolder (encapsulate in quotes if folder is space separated)
    ignore must also include subfolder arg
    e.g: finder[searchstring] {1} [unwantedfolder]
    ignoring multiple folders - separate each folder with a space", 
    GetPaddedHelpString(IgnoreDirectory), IgnoreDirectory);

        public static readonly string HelpDocumentation = string.Format(
@"Uses: finder [searchstring] [args]
{0}
{1}
{2}
{3}
{4}
{5}", UseRegexHelp, IncludeSubDirectoriesHelp, VerboseHelp, IgnoreDirectoryHelp, UseGuiHelp, SetDirectoryHelp);

        private static string GetPaddedHelpString(string[] argString)
        {
            var tmpString = argString[0] + ", " + argString[1];

            for (var i = tmpString.Length; i < padLength; i++)
            {
                tmpString += " ";
            }

            return tmpString;
        }
    }
}
