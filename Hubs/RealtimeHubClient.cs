namespace LIN.Access.Communication.Hubs;

internal sealed class RealtimeHubClient(DeviceOnAccountModel device) : IRealtimeHubClient
{
    private static class HubMethods
    {
        public const string Load = "load";
        public const string JoinGroup = "JoinGroup";
        public const string CommandAction = "SendToDevice";
        public const string SendMessage = "SendMessage";
        public const string OnSendMessage = "sendMessage";
        public const string OnCommand = "#command";
        public const string OnUserInCall = "UserInCall";
    }

    private HubConnection? _conn;
    private bool _handlersConfigured;

    // ===== Handlers internos (listas privadas y seguras) =====
    private readonly HashSet<Action<MessageModel>> _messageHandlers = [];
    private readonly HashSet<Action<string>> _callHandlers = [];
    private readonly HashSet<Action<string>> _commandHandlers = [];

    private ProfileModel? _profile;

    // ===== API pública =====
    public string ConnectionId => _conn?.ConnectionId ?? string.Empty;

    public bool Started => _conn is not null;

    private bool IsConnected => _conn?.State == HubConnectionState.Connected;

    /// <summary>
    /// Suscribir nueva conexión en tiempo real.
    /// </summary>
    public async Task SubscribeAsync(ProfileModel profile, CancellationToken ct = default)
    {
        _profile = profile;
        _conn ??= new HubConnectionBuilder()
                .WithUrl(Service._Service.PathURL("chat"))
                .WithAutomaticReconnect()
                .Build();

        if (_conn.State != HubConnectionState.Connected)
        {
            try
            {
                await _conn.StartAsync(ct).ConfigureAwait(false);
            }
            catch
            {
                return;
            }
        }

        await ConfigureAsync(ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Configurar los eventos del hub.
    /// </summary>
    private async Task ConfigureAsync(CancellationToken ct)
    {
        if (!IsConnected) 
            return;

        // Cargar contexto
        try
        {
            await _conn!.InvokeAsync(HubMethods.Load, _profile, device, ct).ConfigureAwait(false);
            device.ConnectionId = _conn!.ConnectionId ?? string.Empty;
        }
        catch
        {
        }

        if (_handlersConfigured) 
            return;

        // DRY: registrar handlers con helper seguro
        _conn!.On<MessageModel>(HubMethods.OnSendMessage, msg => SafeDispatch(_messageHandlers, msg));
        _conn!.On<string>(HubMethods.OnCommand, cmd => SafeDispatch(_commandHandlers, cmd));
        _conn!.On<string>(HubMethods.OnUserInCall, payload => SafeDispatch(_callHandlers, payload));

        _handlersConfigured = true;
    }

    /// <summary>
    /// Unirse a la sesión de una conversación.
    /// </summary>
    /// <param name="group">Id de la conversación.</param>
    public async Task JoinGroupAsync(int group, CancellationToken ct = default)
    {
        if (!IsConnected) 
            return;
        await InvokeIfConnectedAsync(HubMethods.JoinGroup, ct, group).ConfigureAwait(false);
    }

    /// <summary>
    /// Enviar comando.
    /// </summary>
    /// <param name="deviceId">Id del dispositivo.</param>
    /// <param name="command">Comando</param>
    public async Task SendCommand(string deviceId, string command, CancellationToken ct = default)
    {
        if (!IsConnected)
            return;
        await InvokeIfConnectedAsync(HubMethods.CommandAction, ct, deviceId, command).ConfigureAwait(false);
    }

    // ===== Suscripción a eventos (limpia y simétrica) =====

    public IDisposable OnMessage(Action<MessageModel> handler)
        => AddHandler(_messageHandlers, handler);

    public IDisposable OnCall(Action<string> handler)
        => AddHandler(_callHandlers, handler);

    public IDisposable OnCommand(Action<string> handler)
        => AddHandler(_commandHandlers, handler);

    private async Task InvokeIfConnectedAsync(string method, CancellationToken ct, params object[] args)
    {
        if (!IsConnected) return;
        try
        {
            await _conn!.InvokeAsync(method, args.Append(ct).ToArray()).ConfigureAwait(false);
        }
        catch
        {
        }
    }

    // Añade un handler y devuelve IDisposable para quitarlo sin exponer la lista
    private static IDisposable AddHandler<T>(HashSet<Action<T>> set, Action<T> handler)
    {
        ArgumentNullException.ThrowIfNull(handler);

        lock (set)
        {
            // Si ya existe, NO lo agregamos y devolvemos un Disposable.
            if (!set.Add(handler))
                return NoopDisposable.Instance;
        }

        return new DisposableAction(() =>
        {
            lock (set) set.Remove(handler);
        });
    }

    // Dispara a una copia inmutable para evitar race conditions si alguien desuscribe durante el foreach
    private static void SafeDispatch<T>(HashSet<Action<T>> list, T payload)
    {
        Action<T>[] snapshot;
        lock (list) snapshot = [.. list];

        foreach (var h in snapshot)
        {
            try { h(payload); }
            catch {}
        }
    }

    public async ValueTask DisposeAsync()
    {
        try
        {
            if (_conn is not null)
            {
                // Liberar la conexión de SignalR
                await _conn.DisposeAsync().ConfigureAwait(false);
                _conn = null;
            }
        }
        catch
        {
        }

        // Limpiar estado interno
        lock (_messageHandlers) _messageHandlers.Clear();
        lock (_callHandlers) _callHandlers.Clear();
        lock (_commandHandlers) _commandHandlers.Clear();

        device.ConnectionId = string.Empty;
        _profile = null;
        _handlersConfigured = false;
    }

    private sealed class DisposableAction : IDisposable
    {
        private Action? _dispose;
        public DisposableAction(Action dispose) => _dispose = dispose ?? throw new ArgumentNullException(nameof(dispose));
        public void Dispose()
        {
            _dispose?.Invoke();
            _dispose = null;
        }
    }
}