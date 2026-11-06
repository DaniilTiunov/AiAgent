using AiAgent.Services.Request.Models;
using AiAgent.Services.Response.Models;

namespace AiAgent.Services.Abstractions;

/// <summary>
/// Контракт клиента для обычных и потоковых запросов к Ollama.
/// </summary>
public interface IOllamaClient
{
    /// <summary>
    /// Отправляет запрос к модели и возвращает полный ответ.
    /// </summary>
    Task<ResponseModel> AskAsync(RequestModel request,
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Возвращает текст ответа модели по мере поступления.
    /// </summary>
    IAsyncEnumerable<string> AskStreamAsync(
        RequestModel request,
        CancellationToken cancellationToken = default);
}