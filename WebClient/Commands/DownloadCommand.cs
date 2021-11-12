using System;
using System.Threading.Tasks;
using Spectre.Console;
using Spectre.Console.Cli;
using WebClient.Services;

namespace WebClient.Commands
{
    public class DownloadCommand : AsyncCommand<DownloadCommandSettings>
    {
        private readonly IWebService _webService;
        private readonly IFileService _fileService;

        public DownloadCommand(IWebService webService, IFileService fileService)
        {
            _webService = webService;
            _fileService = fileService;
        }

        public override async Task<int> ExecuteAsync(CommandContext context, DownloadCommandSettings settings)
        {
            try
            {
                await AnsiConsole.Status()
                    .StartAsync("[darkorange3_1]Connecting[/]", async ctx =>
                    {
                        ctx.Spinner(Spinner.Known.Dots);
                        var response = await _webService.GetAsByteArrayAsync(settings.Url);
                        await _fileService.SaveAsync(settings.OutputFile, response);
                        AnsiConsole.MarkupLine($"[gold3_1]Response saved on {settings.OutputFile}![/]");
                    });
            }
            catch (Exception e)
            {
                AnsiConsole.MarkupLine($"[red]{e.Message}[/]");
                return 1;
            }

            return 0;
        }
    }
}