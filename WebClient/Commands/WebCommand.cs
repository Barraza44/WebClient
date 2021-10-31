using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Spectre.Console;
using Spectre.Console.Cli;

namespace WebClient.Commands
{
    public class WebCommand : AsyncCommand<WebCommandSettings>
    {
        private readonly HttpClient _client = new();

        public override async Task<int> ExecuteAsync(CommandContext context, WebCommandSettings settings)
        {
            try
            {
                var response = await _client.GetStringAsync(settings.Url);
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