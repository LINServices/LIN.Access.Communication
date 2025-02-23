namespace LIN.Access.Communication.Controllers;

public static class Profiles
{

    /// <summary>
    /// Iniciar sesión.
    /// </summary>
    /// <param name="cuenta">Cuenta.</param>
    /// <param name="password">Contraseña.</param>
    /// <param name="app">App de contexto.</param>
    public async static Task<ReadOneResponse<AuthModel<ProfileModel>>> Login(string cuenta, string password)
    {

        // Cliente
        Client client = Service.GetClient($"profile/login");

        client.AddParameter("user", cuenta);
        client.AddParameter("password", password);

        return await client.Get<ReadOneResponse<AuthModel<ProfileModel>>>();

    }


    /// <summary>
    /// Login con token.
    /// </summary>
    /// <param name="token">Token de acceso.</param>
    public async static Task<ReadOneResponse<AuthModel<ProfileModel>>> Login(string token)
    {

        // Cliente
        Client client = Service.GetClient($"profile/login/token");

        // Headers.
        client.AddParameter("token", token);

        return await client.Get<ReadOneResponse<Types.Cloud.Identity.Abstracts.AuthModel<ProfileModel>>>();

    }

}