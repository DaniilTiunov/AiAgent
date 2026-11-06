using AiAgent.Services.Tools.Models;

namespace AiAgent.Services.Tools.Interfaces;

/// <summary>
/// Контракт инструмента, возвращающего результат выполнения.
/// </summary>
public interface IFunctionalTool<TResult>
{
    /// <summary>
    /// Выполняет инструмент и возвращает результат.
    /// </summary>
    TResult Execute();
    
    ToolDefinition Definition { get; }
}