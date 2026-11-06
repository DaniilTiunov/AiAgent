using System.Runtime.InteropServices;
using AiAgent.Services.Tools.Interfaces;
using AiAgent.Services.Tools.Models;

namespace AiAgent.Services.Tools.Handlers;

/// <summary>
/// Предоставляет модели сведения о системе сервера.
/// </summary>
public sealed class SystemInfoFunctionalTool : IFunctionalTool<SystemInfoResult>
{
    public ToolDefinition Definition { get; } = new()
    {
        Function = new ToolFunctionDefinition
        {
            Name = "get_system_info",
            Description =
                "Получить ОС, архитектуру, число доступных процессоров " +
                "и версию .NET компьютера, где запущен API приложения."
        }
    };
    
    /// <summary>
    /// Возвращает сведения о системе, где запущено приложение.
    /// </summary>
    public SystemInfoResult Execute()
    {
        return new SystemInfoResult(
            OperatingSystem: RuntimeInformation.OSDescription,
            Architecture: RuntimeInformation.OSArchitecture.ToString(),
            ProcessorCount: Environment.ProcessorCount,
            DotNetVersion: RuntimeInformation.FrameworkDescription);
    }
}