namespace AiAgent.Control.Models;

/// <summary>
/// Ответ API чата с текстом и признаком усечения.
/// </summary>
public sealed class ChatResponse
{
    public required string Text { get; init; }

    public required string Model { get; init; }

    public bool IsTruncated { get; init; }
}