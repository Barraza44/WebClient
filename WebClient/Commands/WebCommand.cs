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
        private readonly IFileService _fileService;

        public WebCommand(IWebService webService, IFileService fileService)
        {
            _webService = webService;
            _fileService = fileService;
        }

        public override async Task<int> ExecuteAsync(CommandContext context, WebCommandSettings settings)
        {
            try
            {
                var response = settings.Method switch
                {
                    "Get" => await _webService.GetAsync(settings.Url),
                    "Post" => await _webService.PostAsync(settings.Url, settings.Body),
                    _ => throw new InvalidOperationException("Unrecognized HTTP method")
                };
                if (!string.IsNullOrEmpty(settings.Output))
                {
                    await _fileService.SaveAsync(settings.Output, response);
                }

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