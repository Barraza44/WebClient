using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;
using WebClient.Commands;
using WebClient.Configurators;
using WebClient.Handlers;
using WebClient.Services;

var services = new ServiceCollection();
services.AddSingleton<IWebService, WebHandler>();
services.AddSingleton<IFileService, FileHandler>();
var registrar = new TypeRegistrar(services);

var app = new CommandApp(registrar);
app.Configure(config =>
{
    config.AddCommand<WebCommand>("web")
        .WithDescription("Connect to a web server to send and receive string-based data")
        .WithExample(new[] { "web", "https://www.microsoft.com" })
        .WithExample(new[] { "web", "https://www.microsoft.com", "--Method", "Get" });
    config.AddCommand<DownloadCommand>("download")
        .WithAlias("down")
        .WithDescription("Connect to a web server to receive binary-based data")
        .WithExample(new[] { "download", "https://www.contoso.com/static/img1.png" });
#if DEBUG
    config.PropagateExceptions();
    config.ValidateExamples();
#endif
});

return app.Run(args);