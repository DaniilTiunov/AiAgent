using AiAgent.Services.Abstractions;
using AiAgent.Services.Client;
using Microsoft.Extensions.DependencyInjection;

namespace AiAgent.DependencyInjection.Extensions;

/// <summary>
/// Расширения для настройки HTTP-клиента Ollama.
/// </summary>
public static class ServiceCollectionHttpExtension
{
    /// <summary>
    /// Регистрирует клиент Ollama с адресом сервера и тайм-аутом.
    /// </summary>
    public static IServiceCollection ConfigureHttpClient(this IServiceCollection services)
    {
        services.AddHttpClient<IOllamaClient, OllamaClient>(client =>
        {
            client.BaseAddress = new Uri("http://172.16.10.230:11434");
            client.Timeout = TimeSpan.FromMinutes(5);
        });

        return services;
    }
}