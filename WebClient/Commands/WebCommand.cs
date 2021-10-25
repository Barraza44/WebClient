using System.Threading.Tasks;
using Spectre.Console.Cli;

namespace WebClient.Commands
{
    public class WebCommand : AsyncCommand
    {
        public override Task<int> ExecuteAsync(CommandContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}