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
        var request = CreateRequest(new KiparisCommands(commandName: "get_app_state"));

        return _kiparisClient.SendAsync(request, cancellationToken);
    }

    public Task<KiparisResponse> GetProcessInfoAsync(
        CancellationToken cancellationToken = default)
    {
        var request = CreateRequest(new KiparisCommands(commandName: "get_process"));
        
        return _kiparisClient.SendAsync(request, cancellationToken);
    }
    
    private KiparisRequest CreateRequest(KiparisCommands commandName)
    {
        return new KiparisRequest
        {
            ProtocolVersion = 1,
            Id = Guid.NewGuid(),
            Command = commandName.CommandName
        };
    }
}