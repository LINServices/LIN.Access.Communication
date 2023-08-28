namespace LIN.Access.Communication.Hubs;


public sealed class ChatHub
{



    //======== Propiedades ========//


    /// <summary>
    /// Conexión del Hub
    /// </summary>
    private HubConnection? HubConnection { get; set; }



    /// <summary>
    /// Obtiene el ID de usuario asignado este dispositivo
    /// </summary>
    public string ID
    {
        get
        {
            return HubConnection?.ConnectionId ?? string.Empty;
        }
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

        }
        catch
        { }

    }




    /// <summary>
    /// Envía un comando
    /// </summary>
    public async Task ConnectMe(ProfileModel profile)
    {


        // Comprueba la conexion
        if (HubConnection?.State != HubConnectionState.Connected)
            return;

        // Ejecucion
        try
        {
            await HubConnection!.InvokeAsync("load", profile);
        }
        catch
        {
        }
    }


    public async void JoinGroup(string group, Action<MessageModel> action)
    {

        // Comprueba la conexión
        if (HubConnection?.State != HubConnectionState.Connected)
            return;

        // Ejecución
        try
        {
            await HubConnection!.InvokeAsync("JoinGroup", group);
            HubConnection.On($"sendMessage-{group}", action);
        }
        catch
        {
        }
    }



    public async void SuscribeToMe(string group, Action<MessageModel> action)
    {

        // Comprueba la conexión
        if (HubConnection?.State != HubConnectionState.Connected)
            return;

        // Ejecución
        try { 
            HubConnection.On($"sendMessage-{group}", action);
        }
        catch
        {
        }
    }



    public async void SendMessage(int me, string group, string message)
    {
        // Comprueba la conexion
        if (HubConnection?.State != HubConnectionState.Connected)
            return;

        // Ejecucion
        try
        {
            await HubConnection!.InvokeAsync("SendMessage", me, group, message);
        }
        catch
        {
        }
    }


}