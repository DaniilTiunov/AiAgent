using System.Text.Json.Serialization;
using AiAgent.Services.Tools.Models;

namespace AiAgent.Services.Response.Models;

/// <summary>
/// Сообщение модели с возможными вызовами инструментов.
/// </summary>
public sealed class ResponseMessageModel
{
    public required string Role { get; set; }
    public string Content { get; set; } = "";

    [JsonPropertyName("tool_calls")]
    public List<ToolCallModel>? ToolCalls { get; init; }
}