using AiAgent.Services.Abstractions;
using AiAgent.Services.Client;
using AiAgent.Services.Kiparis;
using AiAgent.Services.Request.Services;
using AiAgent.Services.Tools;
using AiAgent.Services.Tools.Handlers;
using AiAgent.Services.Tools.Interfaces;
using AiAgent.Services.Tools.KiparisTools;
using AiAgent.Services.Tools.Models;
using Microsoft.Extensions.DependencyInjection;

namespace AiAgent.DependencyInjection.Extensions;

/// <summary>
/// Расширения для регистрации сервисов приложения.
/// </summary>
public static class ServiceCollectionAppServices
{
    /// <summary>
    /// Регистрирует сервисы запросов и доступные инструменты.
    /// </summary>
    public static IServiceCollection ConfigureAppServices(this IServiceCollection services)
    {
        services.AddTransient<IRequestService, RequestService>();
        services.AddTransient<ToolExecuteService>();
        services.AddSingleton<IFunctionalTool<SystemInfoResult>, SystemInfoFunctionalTool>();
        services.AddSingleton<KiparisClient>();
        services.AddTransient<KiparisProjectService>();
        services.AddTransient<IAsyncFunctionalTool<ActiveProjectResult>, ActiveProjectTool>();
        
        return services;
    }
}