namespace LIN.Access.Communication.Controllers;

public static class Messages
{

    /// <summary>
    /// Obtiene los mensajes asociados a una conversación.
    /// </summary>
    /// <param name="idConversation">Id de la conversación.</param>
    /// <param name="lastId">Id del ultimo mensaje a buscar.</param>
    /// <param name="token">Token de acceso.</param>
    public static async Task<ReadAllResponse<MessageModel>> ReadAll(int idConversation, int lastId = 0, string token = "")
    {

        // Cliente
        Client client = Service.GetClient($"conversations/{idConversation}/messages");

        // Headers.
        client.AddHeader("token", token);
        client.AddHeader("lastID", lastId.ToString());

        return await client.Get<ReadAllResponse<MessageModel>>();

    }


    /// <summary>
    /// Enviar mensaje.
    /// </summary>
    /// <param name="id">Id de la conversación.</param>
    /// <param name="guid">Guid del mensaje.</param>
    /// <param name="message">Contenido del mensaje.</param>
    /// <param name="token">Token de acceso.</param>
    public static async Task<CreateResponse> Send(int id, string guid, string message, string token, Types.Communication.Enumerations.MessageTypes type, DateTime? date = null)
    {

        // Cliente
        Client client = Service.GetClient($"conversations/{id}/messages");

        // Headers.
        client.AddHeader("token", token);

        client.AddParameter("guid", guid);
        client.AddParameter("type", (int)type);
        if (date.HasValue)
            client.AddParameter("sendAt", date.Value);

        return await client.Post<CreateResponse>(message);

    }

}