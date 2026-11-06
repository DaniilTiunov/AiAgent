using AiAgent.Control.Extensions;
using AiAgent.Control.Models;
using AiAgent.Services.Abstractions;
using AiAgent.Services.Request.Models;
using Microsoft.AspNetCore.Mvc;

namespace AiAgent.Control.Controllers;

/// <summary>
/// Обрабатывает запросы пользователя к модели.
/// </summary>
[ApiController]
[Route("api/chat")]
public class ChatController : ControllerBase
{
    private readonly IRequestService _requestService;

    /// <summary>
    /// Инициализирует контроллер чата с сервисом запросов.
    /// </summary>
    public ChatController(IRequestService requestService)
    {
        _requestService = requestService;
    }

    /// <summary>
    /// Передаёт сообщение пользователя модели и возвращает ответ чата.
    /// </summary>
    [HttpPost]
    [Route("request")]
    public async Task<ActionResult<ChatResponse>> AskAsync(
        [FromBody] ChatRequest chatRequest,
        CancellationToken cancellationToken)
    {
        var requestModel = new RequestModel
        {
            Model = "qwen3-brief"
        };

        requestModel.Messages = 
        [
            new RequestMessageModel
            {
                Content = chatRequest.Message
            }
        ];

        var response = await _requestService.AskAsync(
            requestModel,
            cancellationToken);

        return Ok(response.ToChatResponse());
    }
}