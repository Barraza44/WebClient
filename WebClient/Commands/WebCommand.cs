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
                if (e.InnerException is not null) AnsiConsole.MarkupLine($"[red]{e.InnerException.Message}[/]");
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
                HttpMethod.Get => await _webService.GetAsync(settings.Url, settings.Headers),

                HttpMethod.Post when !string.IsNullOrWhiteSpace(inputFileData) && fileType == ".json" => await
                    _webService
                        .PostAsJsonAsync(settings.Url, inputFileData, settings.Headers),

                HttpMethod.Post when !string.IsNullOrWhiteSpace(inputFileData) => await _webService.PostAsync(
                    settings.Url,
                    inputFileData, settings.Headers),

                HttpMethod.Post when settings.IsJson => await _webService.PostAsJsonAsync(settings.Url, settings.Body,
                    settings.Headers),

                HttpMethod.Post when settings.IsForm => await _webService.PostAsFormAsync(settings.Url, settings.Body,
                    settings.Headers),

                HttpMethod.Post => await _webService.PostAsync(settings.Url, settings.Body, settings.Headers),

                HttpMethod.Put when !string.IsNullOrWhiteSpace(inputFileData) && fileType == ".json" =>
                    await _webService.PutAsJsonAsync(settings.Url, inputFileData, settings.Headers),
                
                HttpMethod.Put when !string.IsNullOrWhiteSpace(inputFileData) && settings.IsForm =>
                    await _webService.PutAsFormAsync(settings.Url, inputFileData, settings.Headers),
                
                HttpMethod.Put when !string.IsNullOrWhiteSpace(inputFileData) => await _webService.PutAsFormAsync(
                    settings.Url, inputFileData, settings.Headers),
                
                HttpMethod.Put => await _webService.PutAsync(settings.Url, settings.Body, settings.Headers),

                HttpMethod.Patch when !string.IsNullOrWhiteSpace(inputFileData) && fileType == ".json" =>
                    await _webService.PatchAsJsonAsync(settings.Url, inputFileData, settings.Headers),
                
                HttpMethod.Patch when !string.IsNullOrWhiteSpace(inputFileData) && settings.IsForm =>
                    await _webService.PatchAsFormAsync(settings.Url, inputFileData, settings.Headers),
                
                HttpMethod.Patch when !string.IsNullOrWhiteSpace(inputFileData) => await _webService.PatchAsync(
                    settings.Url, inputFileData, settings.Headers),
                
                HttpMethod.Patch => await _webService.PatchAsync(settings.Url, settings.Body, settings.Headers),

                HttpMethod.Delete => await _webService.DeleteAsync(settings.Url, settings.Headers),
                _ => throw new InvalidOperationException("Unrecognized HTTP method")
            };
        }
    }
}