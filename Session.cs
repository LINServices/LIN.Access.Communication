namespace LIN.Access.Communication;


public sealed class Session
{


    /// <summary>
    /// Token de acceso.
    /// </summary>
    public string Token { get; set; } = string.Empty;



    /// <summary>
    /// Token de Identity.
    /// </summary>
    public string AccountToken { get; set; }



    /// <summary>
    /// Información del perfil.
    /// </summary>
    public ProfileModel Profile { get; private set; } = new();



    /// <summary>
    /// Información de la cuenta.
    /// </summary>
    public AccountModel Account { get; private set; } = new();



    /// <summary>
    /// Si la sesión es activa
    /// </summary>
    public static bool IsAccountOpen { get => Instance.Account.Id > 0; }



    /// <summary>
    /// Si la sesión es activa
    /// </summary>
    public static bool IsOpen { get => Instance.Profile.ID > 0; }




    /// <summary>
    /// Recarga o inicia una sesión
    /// </summary>
    public static async Task<(Session? Sesion, Responses Response)> LoginWith(string user, string password)
    {

        // Cierra la sesión Actual
        CloseSession();

        // Validación de user
        var response = await Controllers.Profiles.Login(user, password, "Q333Q");


        if (response.Response != Responses.Success)
            return (null, response.Response);


        // Datos de la instancia
        Instance.Profile = response.Model.Profile;
        Instance.Account = response.Model.Account;

        Instance.Token = response.Token;
        Instance.AccountToken = response.Model.TokenCollection["identity"];

        return (Instance, Responses.Success);

    }



    /// <summary>
    /// Recarga o inicia una sesión
    /// </summary>
    public static async Task<(Session? Sesion, Responses Response)> LoginWith(string token)
    {

        // Cierra la sesión Actual
        CloseSession();

        // Validación de user
        var response = await Controllers.Profiles.Login(token);


        if (response.Response != Responses.Success)
            return (null, response.Response);


        // Datos de la instancia
        Instance.Profile = response.Model.Profile;
        Instance.Account = response.Model.Account;

        Instance.Token = response.Token;
        Instance.AccountToken = response.Model.TokenCollection["identity"];

        return (Instance, Responses.Success);

    }




    /// <summary>
    /// Cierra la sesión
    /// </summary>
    public static void CloseSession()
    {
        Instance.Profile = new();
        Instance.Account = new();
        Instance.AccountToken = "";
        Instance.Token = "";
    }






    //==================== Singleton ====================//


    private readonly static Session _instance = new();

    private Session()
    {
        Token = string.Empty;
        AccountToken = string.Empty;
        Profile = new();
    }


    public static Session Instance => _instance;
}
