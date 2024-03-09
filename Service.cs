namespace LIN.Access.Communication;


internal class Service
{

    /// <summary>
    /// Servicio.
    /// </summary>

    public static LIN.Access.Service _Service = new();



    /// <summary>
    /// Obtener un cliente.
    /// </summary>
    public static LIN.Access.Services.Client GetClient(string url) => _Service.GetClient(url);


}