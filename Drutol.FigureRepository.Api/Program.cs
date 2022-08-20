using Autofac;
using Autofac.Extensions.DependencyInjection;
using Drutol.FigureRepository.Api.DataAccess;
using Drutol.FigureRepository.Api.DataAccess.Seeding;
using Drutol.FigureRepository.Api.Infrastructure;
using Drutol.FigureRepository.Api.Infrastructure.Blockchain;
using Drutol.FigureRepository.Api.Infrastructure.Downloads;
using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Api.Models.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Sinks.LiteDB;

namespace Drutol.FigureRepository.Api;

public class Program
{
    private const string LogDatabase = "Filename=DruFiguresLogs.db;Connection=shared";
    private const string LogTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message} {Exception} {Properties} {NewLine}";

    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(ConfigurationAction));
        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration.Enrich.FromLogContext()
                .WriteTo.LiteDB(LogDatabase, "logs")
                .WriteTo.Console(LogEventLevel.Debug, "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message} {Exception} {Properties} {NewLine}");
        });

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.Configure<BlockchainAuthConfig>(builder.Configuration.GetSection(nameof(BlockchainAuthConfig)));
        builder.Services.Configure<PaypalCheckoutConfiguration>(builder.Configuration.GetSection(nameof(PaypalCheckoutConfiguration)));
        builder.Services.Configure<CheckoutDatabaseConfiguration>(builder.Configuration.GetSection(nameof(CheckoutDatabaseConfiguration)));
        builder.Services.Configure<DownloadConfiguration>(builder.Configuration.GetSection(nameof(DownloadConfiguration)));
        builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection(nameof(JwtConfiguration)));

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseAuthorization();
        app.MapControllers();
        app.UseCors(policyBuilder => policyBuilder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().SetIsOriginAllowed(s => true));

        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console(LogEventLevel.Verbose, LogTemplate)
            .WriteTo.LiteDB(LogDatabase, "logs")
            .CreateLogger();

        try
        {
            Log.Information("Starting web host");
            await app.Services.GetRequiredService<IBlockchainAuthProvider>().Initialize();
            await app.RunAsync();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static void ConfigurationAction(ContainerBuilder builder)
    {
        builder.RegisterType<BlockchainAuthProvider>().AsImplementedInterfaces().SingleInstance();
        builder.RegisterType<LoopringCommunicator>().AsImplementedInterfaces().SingleInstance();

        builder.RegisterType<CheckoutDatabase>().AsImplementedInterfaces().SingleInstance();

        builder.RegisterType<PaypalCheckoutProvider>().AsImplementedInterfaces().SingleInstance();
        builder.RegisterType<FigureSeedManager>().AsImplementedInterfaces().SingleInstance();
        builder.RegisterType<NftTransferProvider>().AsImplementedInterfaces().SingleInstance();
        builder.RegisterType<DownloadTokenManager>().AsImplementedInterfaces().SingleInstance();
        builder.RegisterType<DownloadLinkGenerator>().AsImplementedInterfaces().SingleInstance();
        builder.RegisterType<FigureDownloadManager>().AsImplementedInterfaces().SingleInstance();

        builder.RegisterType<GanyuFigureSeed>().AsImplementedInterfaces().SingleInstance();
        builder.RegisterType<AsukaFigureSeed>().AsImplementedInterfaces().SingleInstance();
        builder.RegisterType<LumineFigureSeed>().AsImplementedInterfaces().SingleInstance();
        builder.RegisterType<MadokaFigureSeed>().AsImplementedInterfaces().SingleInstance();

        builder.RegisterType<BlockchainAuthSession>().AsSelf();
    }
}