using System.ComponentModel;
using Spectre.Console.Cli;

namespace WebClient.Commands
{
    public class WebCommandSettings : CommandSettings
    {
        [CommandArgument(0, "<Url>")] 
        [Description("The URL to which the request will be made.")]
        public string Url { get; set; }

        [CommandOption("--Method | -m | -M")]
        [Description("The HTTP verb to use: Get, Post, Put, Patch, Delete. It defaults to Get.")]
        [DefaultValue("Get")]
        public HttpMethod Method { get; set; }

        [CommandOption("--Body | -b | -B")]
        [Description("Body to send on Post, Put requests.")]
        public string Body { get; set; }

        [CommandOption("--Output | -o | -O")]
        [Description("File on which the HTTP response will be saved.")]
        public string Output { get; set; }

        [CommandOption("--Input | -i | -I")]
        [Description("Read a file to be sent as request body on Post and Put requests.")]
        public string Input { get; set; }
        
        [CommandOption("--Form | -f | -F")]
        [Description(@"Sends the body as form data and sets Content-Type to application/x-www-form-urlencoded. Data must be of form 'key1=value&key2=value...' ")]
        public bool IsForm { get; set; }
        
        [CommandOption("--Json | -J| -j")]
        [Description("Sends the body as JSON and sets Content-Type to application/json. This flag is not needed when the input flag is set to a json file.")]
        public bool IsJson { get; set; }

        [CommandOption("--Headers | -H | -h ")]
        [Description("Set the request headers. Use as: \"Header1: Value, Header2: Value...\" ")]
        public string Headers { get; set; }
    }
}