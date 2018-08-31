﻿namespace Finder
{
    public static class Args
    {
        private static int padLength = 11;

        public const string Help = "-help";

        public const string UseRegex = "-rgx";

        public const string IncludeSubDirectories = "-sf";

        public const string IgnoreDirectory = "-ignore";

        //Help Strings
        public static readonly string UseRegexHelp = 
            string.Format("{0} indicates that the search string is a regex string", 
                GetPaddedHelpString(UseRegex));

        public static readonly string IncludeSubDirectoriesHelp = 
            string.Format("{0} search will include subdirectories", 
                GetPaddedHelpString(IncludeSubDirectories));

        public static readonly string IgnoreDirectoryHelp = string.Format(
@"{0} ignore the contents of a specific subfolder (encapsulate in quotes if folder is space separated)
    ignore must also include subfolder arg
    e.g: finder[searchstring] {1} [unwantedfolder]
    ignoring multiple folders requires multiple ignore tags followed by undesired folder", 
    GetPaddedHelpString(IgnoreDirectory), IgnoreDirectory);

        public static readonly string HelpDocumentation = string.Format(
@"Uses: finder [searchstring] [args]
{0}
{1}
{2}", UseRegexHelp, IncludeSubDirectoriesHelp, IgnoreDirectoryHelp);

        private static string GetPaddedHelpString(string argString)
        {
            for (int i = argString.Length; i < padLength; i++)
            {
                argString += " ";
            }

            return argString;
        }
    }
}
