namespace LIN.Access.Communication.Controllers;


public static class Profiles
{



    public async static Task<ReadOneResponse<Types.Auth.Abstracts.AuthModel<ProfileModel>>> Login(string cuenta, string password, string app)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        httpClient.DefaultRequestHeaders.Add("app", app);

        // ApiServer de la solicitud GET
        string url = ApiServer.PathURL("profile/login");


        url = Web.AddParameters(url, new(){
            {"user", cuenta },
             {"password", password }
        });


        try
        {

            // Hacer la solicitud GET
            var response = await httpClient.GetAsync(url);

            // Leer la respuesta como una cadena
            string responseBody = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<ReadOneResponse<Types.Auth.Abstracts.AuthModel<ProfileModel>>>(responseBody);

            return obj ?? new();

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
        }


        return new();





    }



    public async static Task<ReadOneResponse<Types.Auth.Abstracts.AuthModel<ProfileModel>>> Login(string token)
    {

        // Crear HttpClient
        using var httpClient = new HttpClient();

        // ApiServer de la solicitud GET
        string url = ApiServer.PathURL("profile/login/token");


        url = Web.AddParameters(url, new(){
            {"token", token }
        });


        try
        {

            // Hacer la solicitud GET
            var response = await httpClient.GetAsync(url);

            // Leer la respuesta como una cadena
            string responseBody = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<ReadOneResponse<Types.Auth.Abstracts.AuthModel<ProfileModel>>>(responseBody);

            return obj ?? new();

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al hacer la solicitud GET: {e.Message}");
        }


        return new();





    }











}
