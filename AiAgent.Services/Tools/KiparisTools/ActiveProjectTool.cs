using System.Text.Json;
using AiAgent.Services.Client;
using AiAgent.Services.Tools.Interfaces;
using AiAgent.Services.Tools.Models;
using ReportEngine.Contracts.Models.Project;

namespace AiAgent.Services.Tools.KiparisTools;

public class ActiveProjectTool : IAsyncFunctionalTool<ActiveProjectResult>
{
    private readonly KiparisProjectService _kiparisProjectService;

    public ActiveProjectTool(KiparisProjectService kiparisProjectService)
    {
        _kiparisProjectService = kiparisProjectService;
    }

    public async Task<ActiveProjectResult> ExecuteAsync(
        CancellationToken cancellationToken = default)
    {
        var response = await _kiparisProjectService.GetAppStateAsync(cancellationToken);

        if (!response.Success)
            throw new InvalidOperationException(
                response.Error?.Message ?? "Ошибка получения состояния КИПАРИС.");

        var state = response.Data?.Deserialize<AppStateResult>()
                    ?? throw new InvalidOperationException(
                        "КИПАРИС вернул пустые данные.");

        if (state.CurrentProjectId is 0 or null)
            throw new InvalidOperationException("Не получилось получить проект. Id проекта 0");
        
        return new ActiveProjectResult(
            state.CurrentProjectId,
            state.StandsCount);
    }

    public ToolDefinition Definition { get; } = new()
    {
        Function = new ToolFunctionDefinition
        {
            Name = "get_active_project",
            Description =
                "Активный проект КИПАРИС, кол-во стендов, информация по проекту в запущенном" +
                "КИПАРИС"
        }
    };
}