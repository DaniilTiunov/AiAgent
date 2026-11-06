using System.Text.Json.Serialization;
using AiAgent.Services.Tools.Models;

namespace AiAgent.Services.Response.Models;

/// <summary>
/// Ответ Ollama с сообщением и метриками выполнения.
/// </summary>
public sealed class ResponseModel
{
    public required string Model { get; init; }

    [JsonPropertyName("created_at")] public DateTimeOffset CreatedAt { get; init; }

    public required ResponseMessageModel Message { get; init; }

    public bool Done { get; init; }

    [JsonPropertyName("done_reason")] public string? DoneReason { get; init; }

    [JsonPropertyName("total_duration")] public long TotalDuration { get; init; }

    [JsonPropertyName("load_duration")] public long LoadDuration { get; init; }

    [JsonPropertyName("prompt_eval_count")]
    public int PromptEvalCount { get; init; }

    [JsonPropertyName("prompt_eval_cached_count")]
    public int PromptEvalCachedCount { get; init; }

    [JsonPropertyName("prompt_eval_duration")]
    public long PromptEvalDuration { get; init; }

    [JsonPropertyName("eval_count")] public int EvalCount { get; init; }

    [JsonPropertyName("eval_duration")] public long EvalDuration { get; init; }
}