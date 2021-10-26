using System;
using Spectre.Console.Cli;
using WebClient.Commands;

Console.WriteLine("Hello World!");
var app = new CommandApp();
app.Configure(config =>
{
    config.AddCommand<WebCommand>("web")
        .WithDescription("Connect to a web server to send and receive data")
        .WithExample(new[] { "web", "https://www.microsoft.com" })
        .WithExample(new[] { "web", "https://www.microsoft.com", "--Method", "Get" });
#if DEBUG
    config.PropagateExceptions();
    config.ValidateExamples();
#endif
});

return app.Run(args);