using AiAgent.Services.Request.Models;
using AiAgent.Services.Response.Models;
using AiAgent.Services.Tools.Interfaces;

namespace AiAgent.Services.Abstractions;

/// <summary>
/// Контракт сервиса запросов к модели и выполнения инструментов.
/// </summary>
public interface IRequestService
{
    /// <summary>
    /// Отправляет запрос к модели и возвращает полный ответ.
    /// </summary>
    Task<ResponseModel> AskAsync(
        RequestModel request,
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Возвращает текст ответа модели по мере поступления.
    /// </summary>
    IAsyncEnumerable<string> AskStreamAsync(
        string message,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Выполняет переданный инструмент.
    /// </summary>
    TResult ExecuteTool<TResult>(IFunctionalTool<TResult> functionalTool);
}