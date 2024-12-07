using Microsoft.Extensions.DependencyInjection;

namespace LIN.Access.Communication;

public static class Build
{

    /// <summary>
    /// Autenticación de la aplicación.
    /// </summary>
    internal static string Application { get; set; } = string.Empty;


    /// <summary>
    /// Utilizar LIN Communication.
    /// </summary>
    /// <param name="app">Aplicación.</param>
    /// <param name="url">Ruta.</param>
    public static IServiceCollection AddCommunicationService(this IServiceCollection service, string? url = null, string? app = null)
    {
        Service._Service = new();
        Service._Service.SetDefault(url ?? "https://api.communication.linplatform.com/");
        Application = app ?? "default";
        return service;
    }

}