namespace LIN.Access.Communication.Hubs;


public sealed class ChatHub
{

    /// <summary>
    /// Perfil conectado
    /// </summary>
    private ProfileModel Profile { get; set; }



    /// <summary>
    /// Conexión del Hub
    /// </summary>
    private HubConnection? HubConnection { get; set; }



    /// <summary>
    /// Recibe un mensaje
    /// </summary>
    public event EventHandler<MessageModel>? OnReceiveMessage;



    /// <summary>
    /// Obtiene el ID asociado al Hub
    /// </summary>
    public string ID => HubConnection?.ConnectionId ?? string.Empty;




    /// <summary>
    /// Nueva conexión en tiempo real para chat
    /// </summary>
    /// <param name="profile">Perfil</param>
    public ChatHub(ProfileModel profile)
    {
        this.Profile = profile;
    }




    /// <summary>
    /// Conecta el Hub
    /// </summary>
    public async Task Suscribe()
    {
        try
        {
            // Crea la conexión al HUB
            HubConnection = new HubConnectionBuilder()
                 .WithUrl(ApiServer.PathURL("chat"))
                 .WithAutomaticReconnect()
                 .Build();

            // Inicia la conexión
            await HubConnection.StartAsync();

            // Configuración de la conexión
            await Configurate();

        }
        catch
        { }

    }



    /// <summary>
    /// Configuración de la conexión
    /// </summary>
    private async Task Configurate()
    {
        // Comprueba la conexión
        if (HubConnection?.State != HubConnectionState.Connected)
            return;

        // Cache del perfil
        await HubConnection.InvokeAsync("load", Profile);

        // Evento de mensajes
        HubConnection.On<MessageModel>("sendMessage", (e) => OnReceiveMessage?.Invoke(this, e));

    }



    /// <summary>
    /// Une a una conversación
    /// </summary>
    /// <param name="group">ID de la conversación</param>
    public async Task JoinGroup(int group)
    {

        // Comprueba la conexión
        if (HubConnection?.State != HubConnectionState.Connected)
            return;

        // Ejecución
        try
        {
            await HubConnection!.InvokeAsync("JoinGroup", group);
        }
        catch
        {
        }
    }



    /// <summary>
    /// Enviar mensaje
    /// </summary>
    /// <param name="group">ID de la conversación</param>
    /// <param name="message">Mensaje</param>
    public async Task<bool> SendMessage(int group, string message)
    {
        // Comprueba la conexión
        if (HubConnection?.State != HubConnectionState.Connected)
            return false;

        // Ejecución
        try
        {
            await HubConnection!.InvokeAsync("SendMessage", Profile.ID, group, message);
            return true;
        }
        catch
        {
        }
        return false;
    }


}