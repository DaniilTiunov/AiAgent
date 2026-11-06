using System.IO.Pipes;
using AiAgent.Services.Kiparis;
using ReportEngine.Contracts.Messages;

namespace AiAgent.Services.Client;

public sealed class KiparisClient
{
    public async Task<KiparisResponse> SendAsync(
        KiparisRequest request,
        CancellationToken cancellationToken)
    {
        await using var pipe = new NamedPipeClientStream(
            serverName: ".",
            pipeName: "kiparis.automation.v1",
            direction: PipeDirection.InOut,
            options: PipeOptions.Asynchronous);

        // Даём КИПАРИС 3 секунды на подключение.
        await pipe.ConnectAsync(
            timeout: 3000,
            cancellationToken: cancellationToken);

        // После подключения ограничиваем время обмена сообщениями.
        using var timeout =
            CancellationTokenSource.CreateLinkedTokenSource(
                cancellationToken);

        timeout.CancelAfter(TimeSpan.FromSeconds(30));

        try
        {
            using var messages = new PipeMessageStream(pipe);

            await messages.WriteAsync(request, timeout.Token);

            var response = await messages.ReadAsync<KiparisResponse>(
                timeout.Token);

            if (response is null)
            {
                throw new IOException(
                    "КИПАРИС закрыл соединение без ответа.");
            }

            if (response.ProtocolVersion != 1)
            {
                throw new IOException(
                    "КИПАРИС вернул неподдерживаемую версию протокола.");
            }

            if (response.Id != request.Id)
            {
                throw new IOException(
                    "Идентификатор ответа не совпадает с запросом.");
            }

            return response;
        }
        catch (OperationCanceledException)
            when (!cancellationToken.IsCancellationRequested &&
                  timeout.IsCancellationRequested)
        {
            throw new TimeoutException(
                "КИПАРИС не ответил за отведённое время.");
        }
    }
}