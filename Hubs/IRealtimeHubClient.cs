namespace LIN.Access.Communication.Hubs;

public interface IRealtimeHubClient : IAsyncDisposable
{
    string ConnectionId { get; }
    bool Started { get; }
    Task SubscribeAsync(ProfileModel profile, CancellationToken ct = default);
    Task JoinGroupAsync(int group, CancellationToken ct = default);
    Task SendCommand(string deviceId, string command, CancellationToken ct = default);
    IDisposable OnMessage(Action<MessageModel> handler);
    IDisposable OnCall(Action<Types.Communication.DTO.ReceiveCallDTO> handler);
    IDisposable OnCommand(Action<string> handler);
}