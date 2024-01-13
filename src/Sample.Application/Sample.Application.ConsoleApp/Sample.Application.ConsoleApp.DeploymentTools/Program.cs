using Microsoft.Extensions.Hosting;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Parsing;


CommandLineBuilder commandLineBuilder = new();

commandLineBuilder
    .UseDefaults()
    .UseHost(
        _ => Host.CreateDefaultBuilder(),
        hostBuilder =>
        {
            hostBuilder.ConfigureServices((builder, services) =>
            {
                //services.AddOptions<DatabaseAccessOptions>().BindConfiguration("DatabaseAccess");
                //services.AddDataAccess();
            });
        });

await commandLineBuilder.Build().InvokeAsync(args);