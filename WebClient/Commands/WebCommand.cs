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
                var response = await NetSwitch(settings);
                if (!string.IsNullOrEmpty(settings.Output))
                {
                    await _fileService.SaveAsync(settings.Output, response);
                }

                AnsiConsole.WriteLine($"{response}");
            }
            catch (Exception e)
            {
                AnsiConsole.WriteLine(e.Message);
                return -99;
            }

            return 0;
        }

        private async Task<string> NetSwitch(WebCommandSettings settings)
        {
            var inputFileData = "";
            var fileType = "";
            if (!string.IsNullOrEmpty(settings.Input))
            {
                (inputFileData, fileType) = await _fileService.LoadAsync(settings.Input);
            }

            return settings.Method switch
            {
                "Get" => await _webService.GetAsync(settings.Url),

                "Post" when (!string.IsNullOrEmpty(inputFileData)) && fileType == ".json" => await _webService
                    .PostAsJsonAsync(settings.Url, inputFileData),

                "Post" when (!string.IsNullOrEmpty(inputFileData)) => await _webService.PostAsync(settings.Url,
                    inputFileData),

                "Post" => await _webService.PostAsync(settings.Url, settings.Body),
                _ => throw new InvalidOperationException("Unrecognized HTTP method")
            };
        }
    }
}