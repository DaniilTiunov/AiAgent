using AiAgent.Services.Client;
using AiAgent.Services.Kiparis;
using Microsoft.AspNetCore.Mvc;
using ReportEngine.Contracts.Messages;
// ReSharper disable All

namespace AiAgent.Control.Controllers;

[ApiController]
[Route("api/kiparis")]
public class KiparisController : ControllerBase
{
    private readonly KiparisProjectService _kiparisProjectService;
    private readonly ILogger<KiparisController> _logger;

    public KiparisController(
        KiparisProjectService kiparisProjectService,
        ILogger<KiparisController> logger)
    {
        _kiparisProjectService = kiparisProjectService;
        _logger = logger;
    }

    [HttpGet("state")]
    public async Task<ActionResult<KiparisResponse>> GetStateAsync(
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await _kiparisProjectService.GetAppStateAsync(
                cancellationToken);

            _logger.LogInformation("Запрос на получение активного проекта успешен");
            
            return Ok(response);
        }
        catch (TimeoutException)
        {
            return Problem(
                statusCode: 504,
                detail: "Не удалось дождаться подключения или ответа КИПАРИС.");
        }
        catch (IOException)
        {
            return Problem(
                statusCode: 503,
                detail: "Соединение с КИПАРИС недоступно или прервано.");
        }
    }
}