using System.ComponentModel;
using Spectre.Console.Cli;

namespace WebClient.Commands
{
    public class DownloadCommandSettings : CommandSettings
    {
        [CommandArgument(0, "<URL>")]
        [Description("The Url from which the binary file will be downloaded")]
        public string Url { get; set; }

        [CommandArgument(1, "<FilePath>")]
        [Description("File on which the HTTP response will be saved")]
        public string OutputFile { get; set; }
    }
}