namespace LIN.Access.Communication.Controllers;


public static class Conversations
{


    /// <summary>
    /// Crear conversación.
    /// </summary>
    /// <param name="modelo">Modelo.</param>
    public async static Task<CreateResponse> Create(ConversationModel modelo, string token)
    {

        // Cliente
        Client client = Service.GetClient("conversations/create");

        // Headers.
        client.AddHeader("token", token);

        return await client.Post<CreateResponse>(modelo);

    }



    /// <summary>
    /// Obtiene las conversaciones asociadas a un perfil.
    /// </summary>
    /// <param name="token">Token de acceso</param>
    public async static Task<ReadAllResponse<MemberChatModel>> ReadAll(string token, string tokenAuth)
    {

        // Cliente
        Client client = Service.GetClient("conversations/read/all");

        // Headers.
        client.AddHeader("token", token);
        client.AddHeader("tokenAuth", tokenAuth);

        return await client.Get<ReadAllResponse<MemberChatModel>>();

    }



    /// <summary>
    /// Obtiene los integrantes asociados a una conversación.
    /// </summary>
    /// <param name="idConversation">Id de la conversación.</param>
    public async static Task<ReadAllResponse<MemberChatModel>> Members(int idConversation, string token)
    {

        // Cliente
        Client client = Service.GetClient($"conversations/{idConversation}/members");

        // Headers.
        client.AddHeader("token", token);

        return await client.Get<ReadAllResponse<MemberChatModel>>();

    }



    /// <summary>
    /// Obtiene si un perfil esta online.
    /// </summary>
    /// <param name="id">Id del perfil.</param>
    public async static Task<ReadOneResponse<IsOnlineResult>> IsOnline(int id, string token)
    {

        // Cliente
        Client client = Service.GetClient("conversations/isOnline");

        // Headers.
        client.AddHeader("token", token);

        // Parámetros.
        client.AddParameter("id", id.ToString());

        return await client.Get<ReadOneResponse<IsOnlineResult>>();

    }



    /// <summary>
    /// Obtiene la información asociados a los miembros de una conversación.
    /// </summary>
    /// <param name="idConversation">Id de la conversación.</param>
    /// <param name="token">Token de acceso.</param>
    public async static Task<ReadAllResponse<SessionModel<MemberChatModel>>> MembersInfo(int idConversation, string token, string tokenAuth)
    {

        // Cliente
        Client client = Service.GetClient($"conversations/{idConversation}/members/info");

        // Headers.
        client.AddHeader("token", token);
        client.AddHeader("tokenAuth", tokenAuth);

        return await client.Get<ReadAllResponse<SessionModel<MemberChatModel>>>();

    }



    /// <summary>
    /// Buscar perfiles.
    /// </summary>
    /// <param name="pattern">Patron de búsqueda.</param>
    /// <param name="token">Token de acceso Identity.</param>
    public async static Task<ReadAllResponse<SessionModel<ProfileModel>>> SearchProfiles(string pattern, string token)
    {

        // Cliente
        Client client = Service.GetClient($"profile/search");

        // Headers.
        client.AddHeader("token", token);

        // Parámetro.
        client.AddParameter("pattern", pattern);

        return await client.Get<ReadAllResponse<SessionModel<ProfileModel>>>();

    }




    public async static Task<ReadOneResponse<MemberChatModel>> Read(int id, string token, string tokenAuth)
    {

        // Cliente
        Client client = Service.GetClient($"conversations/read/one");

        // Headers.
        client.AddHeader("token", token);
        client.AddHeader("tokenAuth", tokenAuth);

        // Parámetro.
        client.AddParameter("id", id.ToString());

        return await client.Get<ReadOneResponse<MemberChatModel>>();

    }








    public async static Task<CreateResponse> Find(int friend, string token)
    {

        // Cliente
        Client client = Service.GetClient($"conversations/find");

        // Headers.
        client.AddHeader("token", token);
        client.AddHeader("friendId", friend.ToString());

        return await client.Post<CreateResponse>();

    }




    public async static Task<CreateResponse> Insert(int conversation, int profile, string token)
    {

        // Cliente
        Client client = Service.GetClient($"conversations/{conversation}/members/add");

        // Headers.
        client.AddHeader("token", token);

        client.AddParameter("profileId", profile.ToString());

        return await client.Get<CreateResponse>();

    }



}
