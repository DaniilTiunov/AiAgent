namespace AiAgent.Control.Models;

/// <summary>
/// Запрос пользователя к API чата.
/// </summary>
public sealed class ChatRequest
{
    public required string Message { get; init; }
    public required string ModelName { get; init; } = "qwen3-brief";
}