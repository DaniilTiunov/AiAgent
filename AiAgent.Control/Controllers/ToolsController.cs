using AiAgent.Control.Extensions;
using AiAgent.Control.Models;
using AiAgent.Services.Abstractions;
using AiAgent.Services.Request.Services;
using AiAgent.Services.Tools;
using AiAgent.Services.Tools.Interfaces;
using AiAgent.Services.Tools.Models;
using Microsoft.AspNetCore.Mvc;

namespace AiAgent.Control.Controllers;

/// <summary>
/// Обрабатывает запросы чата с инструментом сведений о системе.
/// </summary>
[ApiController]
[Route("api/chat")]
public class ToolsController : ControllerBase
{
    private readonly ToolExecuteService _toolExecute;
    private readonly IFunctionalTool<SystemInfoResult> _systemTool;
    private readonly IAsyncFunctionalTool<ActiveProjectResult> _projectTool;

    /// <summary>
    /// Инициализирует контроллер чата с системным инструментом.
    /// </summary>
    public ToolsController(
        IFunctionalTool<SystemInfoResult> systemTool, 
        ToolExecuteService toolExecute,
        IAsyncFunctionalTool<ActiveProjectResult> projectTool)
    {
        _systemTool = systemTool;
        _toolExecute = toolExecute;
        _projectTool = projectTool;
    }
    
    /// <summary>
    /// Возвращает ответ модели с использованием сведений о системе.
    /// </summary>
    [HttpPost("sysinfo")]
    public async Task<ActionResult<ChatResponse>> AskAsync(
        [FromBody] ChatRequest chatRequest,
        CancellationToken cancellationToken)
    {
        var response = await _toolExecute.ExecuteToolAsync(
            chatRequest.Message,
            chatRequest.ModelName,
            _systemTool,
            cancellationToken);

        return Ok(response.ToChatResponse());
    }
    
    /// <summary>
    /// Возвращает ответ модели с использованием сведений об активном проекте в КИПАРИС.
    /// </summary>
    [HttpPost("kiparis/active-project")]
    public async Task<ActionResult<ChatResponse>> GetActiveProjectAsync(
        [FromBody] ChatRequest chatRequest,
        CancellationToken cancellationToken)
    {
        var response = await _toolExecute.ExecuteToolAsync(
            chatRequest.Message,
            chatRequest.ModelName,
            _projectTool,
            cancellationToken);
        
        return Ok(response.ToChatResponse());
    }
}