namespace LIN.Access.Communication.Services;


internal static class Service
{


    /// <summary>
    /// Url base.
    /// </summary>
    private static string DefaultUrl { get; set; } = "http://api.communication.linapps.co/";



    /// <summary>
    /// Obtiene la Url.
    /// </summary>
    public static string Url => DefaultUrl;



    /// <summary>
    /// Obtener un cliente Http.
    /// </summary>
    public static Client GetClient(string url)
    {

        // Objeto.
        var client = new Client(Url);

        Uri.TryCreate(new Uri(Url), url, out Uri? result);

        client.BaseAddress = result;
        return client;
    }




    /// <summary>
    /// Establecer la Url.
    /// </summary>
    /// <param name="url">Url default.</param>
    public static void SetDefault(string url)
    {
        DefaultUrl = url;
    }



    /// <summary>
    /// Convertir la URL.
    /// </summary>
    /// <param name="url">url</param>
    public static string PathURL(string url)
    {
        Uri.TryCreate(new Uri(Url), url, out Uri? result);
        return result?.ToString() ?? "";
    }


}