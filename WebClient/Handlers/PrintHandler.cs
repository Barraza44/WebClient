using System;
using System.IO;
using System.Threading.Tasks;
using Spectre.Console;
using WebClient.Services;

namespace WebClient.Handlers
{
    public class PrintHandler : IPrintService
    {
        public void PrintData(string data) => AnsiConsole.WriteLine(data);

        public void PrintError(Exception error) => AnsiConsole.WriteException(error);
        public async Task PrintDataProgress(Stream dataStream)
        {
            var buffer = new byte[dataStream.Length];
            await AnsiConsole.Progress()
                .StartAsync(async ctx =>
                {
                    var downloadTask = ctx.AddTask("[darkturquoise]Fetching[/]", maxValue: dataStream.Length);
                    var bufferData = await dataStream.ReadAsync(buffer);
                    
                    while (!ctx.IsFinished)
                    {
                        downloadTask.Increment(bufferData);
                    }
                });
        }
        
    }
}