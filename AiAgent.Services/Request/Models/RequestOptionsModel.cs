using System.Text.Json.Serialization;

namespace AiAgent.Services.Request.Models;

/// <summary>
/// Параметры размера контекста и длины ответа модели.
/// </summary>
public class RequestOptionsModel
{
    [JsonPropertyName("num_ctx")] public int NumCtx { get; set; } = 2048;

    [JsonPropertyName("num_predict")] public int NumPredict { get; set; } = 256;
}