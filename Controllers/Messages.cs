using LIN.Types.Emma.Models;

namespace LIN.Access.Communication.Controllers;


public static class Messages
{


    /// <summary>
    /// Obtiene los mensajes asociados a una conversación.
    /// </summary>
    /// <param name="idConversation">Id de la conversación.</param>
    /// <param name="lastId">Id del ultimo mensaje a buscar.</param>
    /// <param name="token">Token de acceso.</param>
    public async static Task<ReadAllResponse<MessageModel>> ReadAll(int idConversation, int lastId = 0, string token = "")
    {

        // Cliente
        Client client = Service.GetClient($"conversations/{idConversation}/messages");

        // Headers.
        client.AddHeader("token", token);
        client.AddHeader("lastID", lastId.ToString());

        return await client.Get<ReadAllResponse<MessageModel>>();

    }



    /// <summary>
    /// Preguntar a Emma.
    /// </summary>
    /// <param name="token">Preguntar a Emma.</param>
    /// <param name="token">Token de acceso.</param>
    public async static Task<ReadOneResponse<ResponseIAModel>> ToEmma(string modelo, string token)
    {

        // Cliente
        Client client = Service.GetClient($"emma");

        // Headers.
        client.AddHeader("token", token);

        return await client.Post<ReadOneResponse<ResponseIAModel>>(modelo);

    }



}