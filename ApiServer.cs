﻿namespace LIN.Access.Communication;


public static class ApiServer
{


    /// <summary>
    /// Url del servicio.
    /// </summary>
    public static string GetURL
    {
        get
        {
            return "http://localhost:5270/";
            return "http://lincommunication.somee.com/";
        }
    } 



    /// <summary>
    /// Juntar la url.
    /// </summary>
    /// <param name="value">Url de la API.</param>
    public static string PathURL(string value)
    {
        return GetURL + value;
    }


}