using AiAgent.Services.Abstractions;
using AiAgent.Services.Request.Models;
using AiAgent.Services.Response.Models;
using AiAgent.Services.Tools.Interfaces;

namespace AiAgent.Services.Request.Services;

/// <summary>
/// Обрабатывает запросы к модели через клиент Ollama.
/// </summary>
public class RequestService : IRequestService
{
    private readonly IOllamaClient _ollamaClient;

    /// <summary>
    /// Инициализирует сервис запросов к модели.
    /// </summary>
    public RequestService(IOllamaClient ollamaClient)
    {
        _ollamaClient = ollamaClient;
    }

    /// <summary>
    /// Отправляет запрос к модели и возвращает полный ответ.
    /// </summary>
    public async Task<ResponseModel> AskAsync(
        RequestModel requestModel,
        CancellationToken cancellationToken = default)
    {
        return await _ollamaClient.AskAsync(
            requestModel, cancellationToken);
    }

    /// <summary>
    /// Возвращает текст ответа модели по мере поступления.
    /// </summary>
    public IAsyncEnumerable<string> AskStreamAsync(
        string message, 
        CancellationToken cancellationToken = default)
    {
        return _ollamaClient.AskStreamAsync(
            CreateRequest(message), cancellationToken);
    }

    /// <summary>
    /// Выполняет переданный инструмент.
    /// </summary>
    public TResult ExecuteTool<TResult>(IFunctionalTool<TResult> functionalTool)
    {
        return functionalTool.Execute();
    }

    /// <summary>
    /// Создаёт запрос к модели по умолчанию с системным сообщением.
    /// </summary>
    private RequestModel CreateRequest(string message)
    {
        return new RequestModel
        {
            Model = "qwen3-brief",
            Messages =
            [
                new RequestMessageModel
                {
                    Role = "system",
                    Content = message
                }
            ]
        };
    }
}