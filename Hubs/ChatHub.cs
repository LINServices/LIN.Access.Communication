namespace LIN.Access.Communication.Hubs;

/// <summary>
/// Nueva conexión en tiempo real para chat
/// </summary>
/// <param name="profile">Profile</param>
public sealed class ChatHub(ProfileModel profile)
{

    /// <summary>
    /// Profile conectado
    /// </summary>
    private ProfileModel Profile { get; set; } = profile;


    /// <summary>
    /// Conexión del Hub
    /// </summary>
    private HubConnection? HubConnection { get; set; }


    /// <summary>
    /// Recibe un mensaje
    /// </summary>
    public List<Action<MessageModel>> OnReceiveMessage = new();


    /// <summary>
    /// Obtiene el Id asociado al Hub
    /// </summary>
    public string ID => HubConnection?.ConnectionId ?? string.Empty;


    /// <summary>
    /// Conecta el Hub
    /// </summary>
    public async Task Suscribe()
    {
        try
        {
            // Crea la conexión al HUB
            HubConnection = new HubConnectionBuilder()
                 .WithUrl(Service._Service.PathURL("chat"))
                 .WithAutomaticReconnect()
                 .Build();

            // Inicia la conexión
            await HubConnection.StartAsync();

            // Configuración de la conexión
            await Configurate();

        }
        catch (Exception)
        {
        }
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
        HubConnection.On<MessageModel>("sendMessage", (e) =>
        {
            foreach (var a in OnReceiveMessage)
                a.Invoke(e);
        });
    }


    /// <summary>
    /// Une a una conversación
    /// </summary>
    /// <param name="group">Id de la conversación</param>
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
    /// <param name="group">Id de la conversación</param>
    /// <param name="message">Mensaje</param>
    public async Task<bool> SendMessage(int group, string message, string guid, string token)
    {

        // Comprueba la conexión
        if (HubConnection?.State != HubConnectionState.Connected)
            return await SendMessageApi(group, guid, message, token);

        _ = SendMessageSignal(group, message, guid);
        return false;

    }


    /// <summary>
    /// Enviar mensaje
    /// </summary>
    /// <param name="group">Id de la conversación</param>
    /// <param name="message">Mensaje</param>
    private async Task<bool> SendMessageSignal(int group, string message, string guid, DateTime? date = null)
    {

        // Comprueba la conexión
        if (HubConnection?.State != HubConnectionState.Connected)
            return false;

        // Ejecución
        try
        {
            await HubConnection!.InvokeAsync("SendMessage", Profile.Id, group, message, guid, date);
            return true;
        }
        catch
        {
        }
        return false;
    }


    /// <summary>
    /// Enviar mensaje atreves del API.
    /// </summary>
    /// <param name="group">Id del grupo.</param>
    /// <param name="guid">Id del mensaje.</param>
    /// <param name="message">Contenido del mensaje.</param>
    /// <param name="token">Token de acceso.</param>
    private async Task<bool> SendMessageApi(int group, string guid, string message, string token, DateTime? date = null)
    {
        var response = await LIN.Access.Communication.Controllers.Messages.Send(group, guid, message, token, date);
        return response.Response == Responses.Success;
    }

}