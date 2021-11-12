using System;
using System.IO;
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
            if (Path.GetExtension(new Uri(settings.Url).AbsolutePath).Contains("."))
            {
                AnsiConsole.MarkupLine("[red]Warning:[/] Outputting binary data can cause issues with your terminal. " +
                                       "Use the [blue]download[/] command to save it as a file.");
                return 0;
            }

            try
            {
                await AnsiConsole.Status()
                    .StartAsync("[darkturquoise]Fetching...[/]", async ctx =>
                    {
                        ctx.Spinner(Spinner.Known.Dots4);
                        var response = await NetSwitch(settings);
                        AnsiConsole.WriteLine(response);
                        if (!string.IsNullOrEmpty(settings.Output))
                        {
                            await _fileService.SaveAsync(settings.Output, response);
                            AnsiConsole.MarkupLine($"[gold3_1]Response saved on {settings.Output}![/]");
                        }
                    });
            }
            catch (Exception e)
            {
                AnsiConsole.MarkupLine($"[red]{e.Message}[/]");
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// Selects a HTTP method based on the command flags
        /// </summary>
        /// <param name="settings">The object containing the command flags and options</param>
        /// <returns>The appropriate HTTP method depending on the used flags</returns>
        /// <exception cref="InvalidOperationException">If an unsupported or invalid HTTP method is provided</exception>
        private async Task<string> NetSwitch(WebCommandSettings settings)
        {
            var inputFileData = "";
            var fileType = "";
            if (!string.IsNullOrEmpty(settings.Input))
                (inputFileData, fileType) = await _fileService.LoadAsync(settings.Input);

            return settings.Method switch
            {
                "Get" => await _webService.GetAsync(settings.Url),

                "Post" when !string.IsNullOrEmpty(inputFileData) && fileType == ".json" => await _webService
                    .PostAsJsonAsync(settings.Url, inputFileData),

                "Post" when !string.IsNullOrEmpty(inputFileData) => await _webService.PostAsync(settings.Url,
                    inputFileData),

                "Post" => await _webService.PostAsync(settings.Url, settings.Body),
                _ => throw new InvalidOperationException("Unrecognized HTTP method")
            };
        }
    }
}