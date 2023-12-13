namespace LIN.Access.Communication.Services;


internal class Client : HttpClient
{



    /// <summary>
    /// Nuevo cliente.
    /// </summary>
    public Client(string? url = null)
    {
        try
        {
            BaseAddress = new Uri(url ?? "");
        }
        finally
        {
        }
    }




    /// <summary>
    /// Agregar parámetro a la url
    /// </summary>
    /// <param name="name">Nombre</param>
    /// <param name="value">Valor</param>
    public void AddParameter(string name, string value)
    {
        string url = Web.AddParameters(BaseAddress?.ToString() ?? "", new()
        {
           {name, value }
        });

        BaseAddress = new Uri(url);
    }



    /// <summary>
    /// Agregar parámetros a la url
    /// </summary>
    /// <param name="parameters">Parámetros</param>
    public void AddParameter(Dictionary<string, string> parameters)
    {
        string result = Web.AddParameters(BaseAddress?.ToString() ?? "", parameters);
        BaseAddress = new Uri(result);
    }



    /// <summary>
    /// Agregar un header.
    /// </summary>
    /// <param name="name">Nombre</param>
    /// <param name="value">Valor</param>
    public void AddHeader(string name, string value)
    {
        DefaultRequestHeaders.Add(name, value);
    }



    /// <summary>
    /// Enviar solicitud [GET]
    /// </summary>
    public async Task<T> Get<T>() where T : new()
    {

        // Resultado.
        var result = await GetAsync(string.Empty);

        // Respuesta
        var response = await result.Content.ReadAsStringAsync();

        // Objeto
        T @object = Deserialize<T>(response);

        // Respuesta.
        return @object;
    }



    /// <summary>
    /// Enviar solicitud [POST]
    /// </summary>
    /// <param name="body">Body de documento.</param>
    public async Task<T> Patch<T>(object? body = null) where T : new()
    {

        // Body en JSON.
        string json = JsonSerializer.Serialize(body ?? new { });

        // Contenido.
        StringContent content = new(json, Encoding.UTF8, "application/json");

        // Resultado.
        var result = await PatchAsync(string.Empty, content);

        // Respuesta
        var response = await result.Content.ReadAsStringAsync();

        // Objeto
        T @object = Deserialize<T>(response);

        // Respuesta.
        return @object;
    }



    /// <summary>
    /// Enviar solicitud [POST]
    /// </summary>
    /// <param name="body">Body de documento.</param>
    public async Task<T> Post<T>(object? body = null) where T : new()
    {

        // Body en JSON.
        string json = JsonSerializer.Serialize(body);

        // Contenido.
        StringContent content = new(json, Encoding.UTF8, "application/json");

        // Resultado.
        var result = await PostAsync(string.Empty, content);

        // Respuesta
        var response = await result.Content.ReadAsStringAsync();

        // Objeto
        T @object = Deserialize<T>(response);

        // Respuesta.
        return @object;
    }



    /// <summary>
    /// Enviar solicitud [PUT]
    /// </summary>
    /// <param name="body">Body de documento.</param>
    public async Task<T> Put<T>(object? body = null) where T : new()
    {

        // Body en JSON.
        string json = JsonSerializer.Serialize(body);

        // Contenido.
        StringContent content = new(json, Encoding.UTF8, "application/json");

        // Resultado.
        var result = await PutAsync(string.Empty, content);

        // Respuesta
        var response = await result.Content.ReadAsStringAsync();

        // Objeto
        T @object = Deserialize<T>(response);

        // Respuesta.
        return @object;
    }



    /// <summary>
    /// Enviar solicitud [DELETE]
    /// </summary>
    public async Task<T> Delete<T>() where T : new()
    {

        // Resultado.
        var result = await DeleteAsync(string.Empty);

        // Respuesta
        var response = await result.Content.ReadAsStringAsync();

        // Objeto
        T @object = Deserialize<T>(response);

        // Respuesta.
        return @object;

    }



    /// <summary>
    /// Obtener una respuesta.
    /// </summary>
    /// <typeparam name="T">Tipo de la respuesta.</typeparam>
    /// <param name="content">Contenido.</param>
    public static T Deserialize<T>(string content) where T : new()
    {
        try
        {
            // Objeto
            T @object = JsonSerializer.Deserialize<T>(content) ?? new();
            return @object;
        }
        catch (Exception)
        {
        }

        return new();
    }



}