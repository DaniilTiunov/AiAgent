using System.Text.Json;
using System.Text.Json.Serialization;
using AiAgent.Services.Tools.Models;

namespace AiAgent.Services.Request.Models;

/// <summary>
/// Запрос к Ollama с сообщениями, настройками и инструментами.
/// </summary>
public class RequestModel
{
    public required string Model { get; set; }

    public bool Stream { get; set; } = false;

    public bool Think { get; set; } = false;

    [JsonPropertyName("keep_alive")] public string KeepAlive { get; set; } = "5m";

    public List<RequestMessageModel> Messages { get; set; } = [];

    public RequestOptionsModel Options { get; set; } = new();
    
    public List<ToolDefinition> Tools { get; set; } = [];
}