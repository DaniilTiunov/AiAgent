using AiAgent.Services.Tools.Models;

namespace AiAgent.Services.Tools.Interfaces;

public interface IAsyncFunctionalTool<TResult>
{
    Task<TResult> ExecuteAsync(
        CancellationToken cancellationToken = default);

    ToolDefinition Definition { get; }
}