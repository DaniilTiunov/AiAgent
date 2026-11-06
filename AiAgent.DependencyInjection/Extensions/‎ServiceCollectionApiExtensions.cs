using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace AiAgent.DependencyInjection.Extensions;

/// <summary>
/// Расширения для регистрации контроллеров API.
/// </summary>
public static class ServiceCollectionApiExtensions
{
    /// <summary>
    /// Регистрирует контроллеры из указанной сборки.
    /// </summary>
    public static IServiceCollection ConfigureApi(
        this IServiceCollection services,
        Assembly controllersAssembly)
    {
        services
            .AddControllers()
            .AddApplicationPart(controllersAssembly);

        return services;
    }
}