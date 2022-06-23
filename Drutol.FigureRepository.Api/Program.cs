using Drutol.FigureRepository.Api.Infrastructure;
using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Api.Models.Configuration;

namespace Drutol.FigureRepository.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.Configure<BlockchainAuthConfig>(builder.Configuration.GetSection(nameof(BlockchainAuthConfig)));

        builder.Services.AddSingleton<IBlockchainAuthProvider, BlockchainAuthProvider>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseAuthorization();
        app.MapControllers();
        app.UseCors(policyBuilder => policyBuilder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

        app.Run();
    }
}