using LIN.Access.Communication.Hubs;
using Microsoft.Extensions.DependencyInjection;

namespace LIN.Access.Communication;

public static class Build
{
    /// <summary>
    /// Utilizar LIN Communication.
    /// </summary>
    /// <param name="url">Ruta.</param>
    public static IServiceCollection AddCommunicationService(this IServiceCollection service, string? url = null)
    {
        service.AddSingleton<DeviceOnAccountModel>();
        service.AddScoped<IRealtimeHubClient, RealtimeHubClient>();

        Service._Service = new();
        Service._Service.SetDefault(url ?? "https://api.linplatform.com/communication/");
        return service;
    }
}