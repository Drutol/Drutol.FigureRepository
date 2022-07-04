using Autofac;
using Autofac.Extensions.DependencyInjection;
using Drutol.FigureRepository.Api.DataAccess.Seeding;
using Drutol.FigureRepository.Api.Infrastructure;
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
        builder.Services.Configure<BraintreeConfiguration>(builder.Configuration.GetSection(nameof(BraintreeConfiguration)));


        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseAuthorization();
        app.MapControllers();
        app.UseCors(policyBuilder => policyBuilder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

        app.Services.GetRequiredService<IBlockchainAuthProvider>().Initialize();

        app.Run();
    }

    private static void ConfigurationAction(ContainerBuilder builder)
    {
        builder.RegisterType<BlockchainAuthProvider>().AsImplementedInterfaces().SingleInstance();
        builder.RegisterType<LoopringCommunicator>().AsImplementedInterfaces().SingleInstance();

        builder.RegisterType<BraintreeProvider>().AsImplementedInterfaces().SingleInstance();
        builder.RegisterType<FigureSeedManager>().AsImplementedInterfaces().SingleInstance();

        builder.RegisterType<GanyuFigureSeed>().AsImplementedInterfaces().SingleInstance();
        builder.RegisterType<AsukaFigureSeed>().AsImplementedInterfaces().SingleInstance();
        builder.RegisterType<LumineFigureSeed>().AsImplementedInterfaces().SingleInstance();
        builder.RegisterType<MadokaFigureSeed>().AsImplementedInterfaces().SingleInstance();

        builder.RegisterType<BlockchainAuthSession>().AsSelf();
    }
}