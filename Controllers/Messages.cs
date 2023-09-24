namespace LIN.Access.Communication.Controllers;


public static class Messages
{


    /// <summary>
    /// Obtiene los mensajes asociados a una conversación
    /// </summary>
    /// <param name="idConversation">ID de la conversación</param>
    public async static Task<ReadAllResponse<MessageModel>> ReadAll(int idConversation, int lastId = 0, string token = "")
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        httpClient.DefaultRequestHeaders.Add("lastID", lastId.ToString());
        httpClient.DefaultRequestHeaders.Add("token", token);

        // ApiServer de la solicitud GET
        string url = ApiServer.PathURL($"conversations/{idConversation}/messages");

        try
        {

            // Hacer la solicitud GET
            var response = await httpClient.GetAsync(url);

            // Leer la respuesta como una cadena
            string responseBody = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<ReadAllResponse<MessageModel>>(responseBody);

            return obj ?? new();

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
        }


        return new();





    }


}