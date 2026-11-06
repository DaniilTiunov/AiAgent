using ReportEngine.Contracts.Commands;
using ReportEngine.Contracts.Messages;

namespace AiAgent.Services.Client;

public class KiparisProjectService
{
    private readonly KiparisClient _kiparisClient;

    public KiparisProjectService(KiparisClient kiparisClient)
    {
        _kiparisClient = kiparisClient;
    }

    public Task<KiparisResponse> GetAppStateAsync(
        CancellationToken cancellationToken = default)
    {
        var request = new KiparisRequest
        {
            ProtocolVersion = 1,
            Id = Guid.NewGuid(),
            Command = KiparisCommands.GetAppState
        };

        return _kiparisClient.SendAsync(request, cancellationToken);
    }
}