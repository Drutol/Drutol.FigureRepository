using Autofac;
using Autofac.Extensions.DependencyInjection;
using Drutol.FigureRepository.Api.DataAccess;
using Drutol.FigureRepository.Api.DataAccess.Seeding;
using Drutol.FigureRepository.Api.Infrastructure;
using Drutol.FigureRepository.Api.Infrastructure.Blockchain;
using Drutol.FigureRepository.Api.Infrastructure.Downloads;
using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Api.Models.Configuration;

namespace Drutol.FigureRepository.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(ConfigurationAction));

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

        app.Services.GetRequiredService<IBlockchainAuthProvider>().Initialize();

        app.Run();
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