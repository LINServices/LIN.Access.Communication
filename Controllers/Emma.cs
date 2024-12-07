namespace LIN.Access.Communication.Controllers;

internal class Emma
{

    /// <summary>
    /// Preguntar a Emma.
    /// </summary>
    /// <param name="token">Preguntar a Emma.</param>
    /// <param name="token">Token de acceso.</param>
    public static async Task<ReadOneResponse<LIN.Types.Cloud.OpenAssistant.Models.EmmaSchemaResponse>> ToEmma(string modelo, string token)
    {

        // Cliente
        Client client = Service.GetClient($"emma");

        // Headers.
        client.AddHeader("tokenAuth", token);

        return await client.Post<ReadOneResponse<Types.Cloud.OpenAssistant.Models.EmmaSchemaResponse>>(modelo);

    }

}