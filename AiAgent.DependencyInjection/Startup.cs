using System.Reflection;
using AiAgent.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace AiAgent.DependencyInjection;

/// <summary>
/// Настраивает и создаёт веб-хост приложения.
/// </summary>
public static class Startup
{
    /// <summary>
    /// Создаёт хост с сервисами приложения и контроллерами API.
    /// </summary>
    public static IHost BuildHost(
        string[] args,
        Assembly controllersAssembly)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureServices(services =>
                {
                    services.ConfigureAppServices();
                    services.ConfigureHttpClient();
                    services.ConfigureApi(controllersAssembly);
                });

                webBuilder.Configure(ConfigurePipeline);
            })
            .Build();
    }

    /// <summary>
    /// Настраивает маршрутизацию и обработку запросов контроллерами.
    /// </summary>
    private static void ConfigurePipeline(IApplicationBuilder app)
    {
        app.UseRouting();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}