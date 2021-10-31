using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Spectre.Console;
using Spectre.Console.Cli;
using WebClient.Services;

namespace WebClient.Commands
{
    public class WebCommand : AsyncCommand<WebCommandSettings>
    {
        private readonly IWebService _webService;

        public WebCommand(IWebService webService)
        {
            _webService = webService;
        }

        public override async Task<int> ExecuteAsync(CommandContext context, WebCommandSettings settings)
        {
            try
            {
                var response = await _webService.GetAsync(settings.Url);
                AnsiConsole.WriteLine($"{response}");
            }
            catch (Exception e)
            {
                AnsiConsole.MarkupLine($"[red]{e.Message}[/]");
                return -99;
            }

            return 0;
        }
    }
}