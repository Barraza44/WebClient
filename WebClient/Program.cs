using System;
using Spectre.Console.Cli;

Console.WriteLine("Hello World!");
var app =  new CommandApp();
app.Configure(config =>
{
#if DEBUG
    config.PropagateExceptions();
    config.ValidateExamples();
#endif
});