using AiAgent.Control.Models;
using AiAgent.Services.Response.Models;

namespace AiAgent.Control.Extensions;

/// <summary>
/// Преобразует ответы модели в формат API чата.
/// </summary>
public static class ChatMappingExtensions
{
    /// <summary>
    /// Преобразует ответ Ollama в ответ API чата.
    /// </summary>
    public static ChatResponse ToChatResponse(this ResponseModel response)
    {
        return new ChatResponse
        {
            Model = response.Model,
            Text = response.Message.Content,
            IsTruncated = response.DoneReason == "length"
        };
    }
}