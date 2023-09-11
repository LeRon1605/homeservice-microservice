using Microsoft.Extensions.Configuration;
using Serilog;

namespace BuildingBlocks.Infrastructure.Serilog;

public static class ApplicationLoggerFactory
{
    public static ILogger CreateSerilogLogger(IConfiguration configuration, string applicationContext)
    {
        var seqServerUrl = configuration["Serilog:SeqServerUrl"];

        return new LoggerConfiguration()
                    .MinimumLevel.Verbose()
                    .Enrich.WithProperty("ApplicationContext", applicationContext)
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .WriteTo.Seq(seqServerUrl)
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();
    }
}