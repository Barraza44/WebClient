using System.Threading.Tasks;
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
            return 0;
        }
    }
}