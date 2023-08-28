namespace LIN.Access.Communication.Controllers;


public static class Messages
{


    public async static Task<ReadAllResponse<MessageModel>> ReadMessage(int cv)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        httpClient.DefaultRequestHeaders.Add("conversacion", cv.ToString());

        // ApiServer de la solicitud GET
        string url = ApiServer.PathURL("conversations/read/messages");



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
