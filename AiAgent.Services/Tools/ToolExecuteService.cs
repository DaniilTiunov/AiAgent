using System.Text.Json;
using AiAgent.Services.Abstractions;
using AiAgent.Services.Request.Models;
using AiAgent.Services.Response.Models;
using AiAgent.Services.Tools.Interfaces;
using AiAgent.Services.Tools.Models;

namespace AiAgent.Services.Tools;

/// <summary>
/// Обрабатывает вызовы инструмента в диалоге с моделью.
/// </summary>
public class ToolExecuteService
{
    private readonly IRequestService _requestService;

    /// <summary>
    /// Инициализирует сервис выполнения инструментов.
    /// </summary>
    public ToolExecuteService(IRequestService requestService)
    {
        _requestService = requestService;
    }
    
    public Task<ResponseModel> ExecuteToolAsync<TResult>(
        string message,
        string model,
        IFunctionalTool<TResult> tool,
        CancellationToken cancellationToken = default)
    {
        return ExecuteCoreAsync<TResult>(
            message,
            model,
            tool.Definition,
            token =>
            {
                token.ThrowIfCancellationRequested();

                return Task.FromResult(tool.Execute());
            },
            cancellationToken);
    }
    
    public Task<ResponseModel> ExecuteToolAsync<TResult>(
        string message,
        string model,
        IAsyncFunctionalTool<TResult> tool,
        CancellationToken cancellationToken = default)
    {
        return ExecuteCoreAsync<TResult>(
            message,
            model,
            tool.Definition,
            token => tool.ExecuteAsync(token),
            cancellationToken);
    }

    /// <summary>
    /// Выполняет запрошенный моделью инструмент и получает итоговый ответ.
    /// </summary>
    private async Task<ResponseModel> ExecuteCoreAsync<TResult>(
        string message,
        string model,
        ToolDefinition definition,
        Func<CancellationToken, Task<TResult>> execute,
        CancellationToken cancellationToken)
    {
        var request = new RequestModel
        {
            Model = model,
            Stream = false,
            Messages =
            [
                new RequestMessageModel
                {
                    Role = "user",
                    Content = message
                }
            ],
            Tools = [definition]
        };
        
        const int maxToolRounds = 3;

        for (var round = 0; round <= maxToolRounds; round++)
        {
            if (round == maxToolRounds)
                request.Tools.Clear();

            var response = await _requestService.AskAsync(
                request,
                cancellationToken);

            var calls = response.Message.ToolCalls;

            if (calls is null || calls.Count == 0)
                return response;

            if (round == maxToolRounds)
            {
                throw new InvalidOperationException(
                    "Модель не завершила работу с инструментами.");
            }

            request.Messages.Add(new RequestMessageModel
            {
                Role = "assistant",
                Content = response.Message.Content,
                ToolCalls = calls
            });

            foreach (var call in calls)
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (call.Function.Name != definition.Function.Name)
                {
                    throw new InvalidOperationException(
                        $"Неизвестный инструмент: {call.Function.Name}");
                }

                var arguments = call.Function.Arguments;

                if (arguments.ValueKind != JsonValueKind.Object ||
                    arguments.EnumerateObject().Any())
                {
                    throw new InvalidOperationException(
                        "Этот инструмент не принимает аргументы.");
                }

                var result = await execute(cancellationToken);

                request.Messages.Add(new RequestMessageModel
                {
                    Role = "tool",
                    ToolName = call.Function.Name,
                    Content = JsonSerializer.Serialize(result)
                });
            }
        }

        throw new InvalidOperationException(
            "Не удалось получить итоговый ответ.");
    }
}