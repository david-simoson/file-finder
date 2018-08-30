namespace Finder
{
    static class Help
    {
        public static string HelpString = 
@"Uses: finder [searchstring] [args]
-rgx	 indicates that the search string is a regex string
-sf      include all subfolders in the search
-ignore  ignore the contents of a specific subfolder (encapsulate in quotes if folder is space separated)
         ignore must follow the '-sf' arg
         e.g: finder [searchstring] -sf -ignore [unwantedfolder]
         ignoring multiple folders requires multiple ignore tags followed by undesired folder";

    }
}
