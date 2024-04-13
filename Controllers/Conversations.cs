namespace LIN.Access.Communication.Controllers;


public static class Conversations
{


    /// <summary>
    /// Crear nueva conversación.
    /// </summary>
    /// <param name="modelo">Modelo.</param>
    /// <param name="token">Token de acceso.</param>
    public async static Task<CreateResponse> Create(ConversationModel modelo, string token)
    {

        // Cliente
        Client client = Service.GetClient("conversations");

        // Headers.
        client.AddHeader("token", token);

        return await client.Post<CreateResponse>(modelo);

    }



    /// <summary>
    /// Obtener las conversaciones de un perfil.
    /// </summary>
    /// <param name="token">Token de acceso.</param>
    /// <param name="tokenAuth">Token de Identity.</param>
    public async static Task<ReadAllResponse<MemberChatModel>> ReadAll(string token, string tokenAuth)
    {

        // Cliente
        Client client = Service.GetClient("conversations/all");

        // Headers.
        client.AddHeader("token", token);
        client.AddHeader("tokenAuth", tokenAuth);

        return await client.Get<ReadAllResponse<MemberChatModel>>();

    }



    /// <summary>
    /// Obtener una conversación.
    /// </summary>
    /// <param name="id">Id de la conversación.</param>
    /// <param name="token">Token de acceso.</param>
    /// <param name="tokenAuth">Token de identity.</param>
    public async static Task<ReadOneResponse<MemberChatModel>> Read(int id, string token, string tokenAuth)
    {

        // Cliente
        Client client = Service.GetClient($"conversations");

        // Headers.
        client.AddHeader("token", token);
        client.AddHeader("tokenAuth", tokenAuth);

        // Parámetro.
        client.AddParameter("id", id.ToString());

        return await client.Get<ReadOneResponse<MemberChatModel>>();

    }



    /// <summary>
    /// Actualizar el nombre.
    /// </summary>
    /// <param name="id">Id de la conversación.</param>
    /// <param name="name">Nuevo nombre.</param>
    /// <param name="token">Token de acceso.</param>
    public async static Task<CreateResponse> UpdateName(int id, string name, string token)
    {

        // Cliente
        Client client = Service.GetClient($"conversations/name");

        // Headers.
        client.AddHeader("token", token);

        // Parámetros.
        client.AddParameter("id", id);
        client.AddParameter("newName", name);

        return await client.Patch<CreateResponse>();

    }



}