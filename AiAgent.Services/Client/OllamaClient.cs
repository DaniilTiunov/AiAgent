using System.Net.Http.Json;
using System.Text.Json;
using AiAgent.Services.Abstractions;
using AiAgent.Services.Request.Models;
using AiAgent.Services.Response.Models;

namespace AiAgent.Services.Client;

/// <summary>
/// Отправляет HTTP-запросы к Ollama и читает ответы.
/// </summary>
public class OllamaClient : IOllamaClient
{
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Инициализирует клиент Ollama с заданным HTTP-клиентом.
    /// </summary>
    public OllamaClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Отправляет запрос к модели и возвращает полный ответ.
    /// </summary>
    public async Task<ResponseModel> AskAsync(
        RequestModel request,
        CancellationToken cancellationToken = default)
    {
        using var response = await _httpClient.PostAsJsonAsync(
            "api/chat",
            request,
            cancellationToken);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<ResponseModel>(
                   cancellationToken)
               ?? throw new InvalidOperationException(
                   "Ollama вернула пустой JSON-ответ.");
    }

    /// <summary>
    /// Возвращает текст ответа модели по мере поступления.
    /// </summary>
    public async IAsyncEnumerable<string> AskStreamAsync(
        RequestModel request, 
        CancellationToken cancellationToken = default)
    {
        var streamRequest = new RequestModel
        {
            Model = request.Model,
            Stream = true,
            Think = request.Think,
            KeepAlive = request.KeepAlive,
            Messages = request.Messages,
            Options = request.Options
        };

        using var httpRequest = new HttpRequestMessage(
            HttpMethod.Post,
            "api/chat")
        {
            Content = JsonContent.Create(streamRequest)
        };
        
        using var response = await _httpClient.SendAsync(
            httpRequest,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken);

        response.EnsureSuccessStatusCode();

        await using var stream = await response.Content.ReadAsStreamAsync(
            cancellationToken);

        using var reader = new StreamReader(stream);

        while (true)
        {
            var line = await reader.ReadLineAsync(cancellationToken);

            if (line is null)
            {
                throw new IOException(
                    "Поток Ollama оборвался до завершения ответа.");
            }

            if (string.IsNullOrWhiteSpace(line))
                continue;

            using var json = JsonDocument.Parse(line);
            var root = json.RootElement;
            
            if (root.TryGetProperty("error", out var error))
            {
                throw new InvalidOperationException(
                    $"Ошибка Ollama: {error.GetString()}");
            }

            if (root.TryGetProperty("message", out var message) &&
                message.TryGetProperty("content", out var content))
            {
                var text = content.GetString();

                if (!string.IsNullOrEmpty(text))
                    yield return text;
            }

            if (root.TryGetProperty("done", out var done) &&
                done.GetBoolean())
            {
                yield break;
            }
        }
    }
}