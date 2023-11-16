namespace LIN.Access.Communication.Controllers;


public static class Conversations
{


    /// <summary>
    /// Crear nueva conversación.
    /// </summary>
    /// <param name="modelo">Modelo.</param>
    public async static Task<CreateResponse> Create(ConversationModel modelo, string token)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        httpClient.DefaultRequestHeaders.Add("token", token);

        // ApiServer de la solicitud GET
        string url = ApiServer.PathURL("conversations/create");
        var json = JsonSerializer.Serialize(modelo);

        try
        {
            // Contenido
            StringContent content = new(json, Encoding.UTF8, "application/json");

            // Envía la solicitud
            var response = await httpClient.PostAsync(url, content);

            // Lee la respuesta del servidor
            var responseContent = await response.Content.ReadAsStringAsync();

            var obj = JsonSerializer.Deserialize<CreateResponse>(responseContent);

            return obj ?? new();

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
        }


        return new();





    }



    /// <summary>
    /// Obtiene las conversaciones asociadas a un perfil.
    /// </summary>
    /// <param name="token">Token de acceso</param>
    public async static Task<ReadAllResponse<MemberChatModel>> ReadAll(string token)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        httpClient.DefaultRequestHeaders.Add("token", token);

        // ApiServer de la solicitud GET
        string url = ApiServer.PathURL("conversations/read/all");

        try
        {

            // Hacer la solicitud GET
            var response = await httpClient.GetAsync(url);

            // Leer la respuesta como una cadena
            string responseBody = await response.Content.ReadAsStringAsync();

            var obj = JsonSerializer.Deserialize<ReadAllResponse<MemberChatModel>>(responseBody);

            return obj ?? new();

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
        }


        return new();





    }






    /// <summary>
    /// Obtiene los mensajes asociados a una conversación
    /// </summary>
    /// <param name="idConversation">ID de la conversación</param>
    public async static Task<ReadAllResponse<MemberChatModel>> Members(int idConversation)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        httpClient.DefaultRequestHeaders.Add("token", "");

        // ApiServer de la solicitud GET
        string url = ApiServer.PathURL($"conversations/{idConversation}/members");

        try
        {

            // Hacer la solicitud GET
            var response = await httpClient.GetAsync(url);

            // Leer la respuesta como una cadena
            string responseBody = await response.Content.ReadAsStringAsync();

            var obj = JsonSerializer.Deserialize<ReadAllResponse<MemberChatModel>>(responseBody);

            return obj ?? new();

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
        }


        return new();





    }




    public async static Task<ReadOneResponse<IsOnlineResult>> IsOnline(int id)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();


        // ApiServer de la solicitud GET
        string url = ApiServer.PathURL($"conversations/isOnline");

        url = Web.AddParameters(url, new()
        {
            {"id", id.ToString() }
        });

        try
        {

            // Hacer la solicitud GET
            var response = await httpClient.GetAsync(url);

            // Leer la respuesta como una cadena
            string responseBody = await response.Content.ReadAsStringAsync();

            var obj = JsonSerializer.Deserialize<ReadOneResponse<IsOnlineResult>>(responseBody);

            return obj ?? new();

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
        }


        return new();





    }





    /// <summary>
    /// Obtiene los mensajes asociados a una conversación
    /// </summary>
    /// <param name="idConversation">ID de la conversación</param>
    public async static Task<ReadAllResponse<SessionModel<MemberChatModel>>> MembersInfo(int idConversation, string token)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        httpClient.DefaultRequestHeaders.Add("token", $"{token}");

        // ApiServer de la solicitud GET
        string url = ApiServer.PathURL($"conversations/{idConversation}/members/info");

        try
        {

            // Hacer la solicitud GET
            var response = await httpClient.GetAsync(url);

            // Leer la respuesta como una cadena
            string responseBody = await response.Content.ReadAsStringAsync();

            var obj = JsonSerializer.Deserialize<ReadAllResponse<SessionModel<MemberChatModel>>>(responseBody);

            return obj ?? new();

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
        }


        return new();





    }



    public async static Task<ReadAllResponse<SessionModel<ProfileModel>>> SearchProfiles(string pattern, string token)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        httpClient.DefaultRequestHeaders.Add("token", $"{token}");

        // ApiServer de la solicitud GET
        string url = ApiServer.PathURL($"profile/search");

        url = Web.AddParameters(url, new()
        {
            {"pattern", pattern }
        });

        try
        {

            // Hacer la solicitud GET
            var response = await httpClient.GetAsync(url);

            // Leer la respuesta como una cadena
            string responseBody = await response.Content.ReadAsStringAsync();

            var obj = JsonSerializer.Deserialize<ReadAllResponse<SessionModel<ProfileModel>>>(responseBody);

            return obj ?? new();

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
        }


        return new();





    }


}
