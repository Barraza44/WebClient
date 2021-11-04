using System.ComponentModel;
using Spectre.Console.Cli;

namespace WebClient.Commands
{
    public class WebCommandSettings : CommandSettings
    {
        [CommandArgument(0, "<Url>")]
        public string Url { get; set; }

        [CommandOption("--Method | -m | -M")]
        [Description("The HTTP verb to use: Get, Post, Put, Patch, Delete")]
        [DefaultValue("Get")]
        public string Method { get; set; }

        [CommandOption("--Body | -b | -B")]
        [Description("Body to send on Post, Put requests")]
        public string Body { get; set; }
        
        [CommandOption("--Output | -o | -O")]
        [Description("File on which the HTTP response will be saved")]
        public string Output { get; set; }

        [CommandOption("--Input | -i | -I")]
        [Description("Read a file to be sent as request body")]
        public string Input { get; set; }
    }
}