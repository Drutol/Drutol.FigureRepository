using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Drutol.FigureRepository.Api.DataAccess;
using Drutol.FigureRepository.Api.DataAccess.Seeding;
using Drutol.FigureRepository.Api.Infrastructure;
using Drutol.FigureRepository.Api.Infrastructure.Blockchain;
using Drutol.FigureRepository.Api.Infrastructure.Downloads;
using Drutol.FigureRepository.Api.Infrastructure.Services;
using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Api.Models;
using Drutol.FigureRepository.Api.Models.Configuration;
using Drutol.FigureRepository.Shared.Admin;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Sinks.LiteDB;

namespace Drutol.FigureRepository.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();

        // Add Autofac
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(ConfigurationAction));

        // Add Logging
        var logConfig = builder.Configuration.GetSection(nameof(LogConfiguration)).Get<LogConfiguration>();
        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration.Enrich.FromLogContext()
                .MinimumLevel.Override("Drutol", LogEventLevel.Verbose)
                .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Hosting.Diagnostics", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Cors", LogEventLevel.Warning)
                .WriteTo.LiteDB(logConfig.LogDatabase, logConfig.LogCollection)
                .WriteTo.Console(LogEventLevel.Verbose, logConfig.LogTemplate);
        });

        // Add Authentication
        var jwtConfig = builder.Configuration.GetSection(nameof(JwtConfiguration)).Get<JwtConfiguration>();
        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.JwtSigningKey))
                };
            });

        // Add Authorization
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthPolicies.AdminPolicy, policyBuilder =>
            {
                policyBuilder.RequireClaim(AdminClaims.AdminClaim);
            });
        });

        // Add configuration
        builder.Services.Configure<BlockchainAuthConfig>(builder.Configuration.GetSection(nameof(BlockchainAuthConfig)));
        builder.Services.Configure<PaypalCheckoutConfiguration>(builder.Configuration.GetSection(nameof(PaypalCheckoutConfiguration)));
        builder.Services.Configure<CheckoutDatabaseConfiguration>(builder.Configuration.GetSection(nameof(CheckoutDatabaseConfiguration)));
        builder.Services.Configure<DownloadConfiguration>(builder.Configuration.GetSection(nameof(DownloadConfiguration)));
        builder.Services.Configure<AdminConfiguration>(builder.Configuration.GetSection(nameof(AdminConfiguration)));
        builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection(nameof(JwtConfiguration)));
        builder.Services.Configure<LogConfiguration>(builder.Configuration.GetSection(nameof(LogConfiguration)));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.UseCors(policyBuilder => policyBuilder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().SetIsOriginAllowed(s => true));

        var logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console(LogEventLevel.Verbose, logConfig.LogTemplate)
            .WriteTo.LiteDB(logConfig.LogDatabase, logConfig.LogCollection)
            .CreateLogger();

        try
        {
            logger.Information("Starting web host");
            await app.Services.GetRequiredService<IBlockchainAuthProvider>().Initialize();
            await app.RunAsync();
        }
        catch (Exception ex)
        {
            logger.Fatal(ex, "Host terminated unexpectedly");
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

        builder.RegisterType<OrderDatabase>().AsImplementedInterfaces().SingleInstance();
        builder.RegisterType<LogDatabase>().AsImplementedInterfaces().SingleInstance();

        builder.RegisterType<AdminService>().AsImplementedInterfaces().SingleInstance();

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

        builder.RegisterType<PendingDeliveryFulfillmentService>().AsImplementedInterfaces().SingleInstance();

        builder.RegisterType<BlockchainAuthSession>().AsSelf();
    }
}