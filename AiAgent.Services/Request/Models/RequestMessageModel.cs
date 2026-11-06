using System.Text.Json.Serialization;
using AiAgent.Services.Tools.Models;

namespace AiAgent.Services.Request.Models;

/// <summary>
/// Сообщение диалога или результат выполнения инструмента.
/// </summary>
public class RequestMessageModel
{
    public string Role { get; set; }
    public string Content { get; set; }
    
    [JsonPropertyName("tool_calls")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<ToolCallModel>? ToolCalls { get; set; }

    [JsonPropertyName("tool_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ToolName { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Thinking { get; set; }
}