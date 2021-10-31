using System;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;
using WebClient.Commands;
using WebClient.Configurators;
using WebClient.Services;

var services = new ServiceCollection();
services.AddSingleton<IWebService, WebService>();
var registrar = new TypeRegistrar(services);

var app = new CommandApp(registrar);
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