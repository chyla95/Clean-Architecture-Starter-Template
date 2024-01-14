using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sample.Architecture.Infrastructure.DataStorage.Extensions.DependencyInjection;
using Sample.Architecture.Infrastructure.DataStorage.Options;
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
                services.AddOptions<DatabaseAccessOptions>().BindConfiguration("DatabaseAccess");
                services.AddDataStorage();
            });
        });

await commandLineBuilder.Build().InvokeAsync(args);