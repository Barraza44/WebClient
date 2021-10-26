using System.ComponentModel;
using Spectre.Console.Cli;

namespace WebClient.Commands
{
    public class WebCommandSettings : CommandSettings
    {
        [CommandArgument(0, "<Url>")]
        public string Url { get; set; }

        [CommandOption("--Method | -m | -M")]
        [DefaultValue("Get")]
        public string Method { get; set; }
    }
}