namespace LIN.Access.Communication;

public class Build
{



    public static void Init()
    {
        Service._Service = new();
        Service._Service.SetDefault("http://api.communication.linapps.co/");
        //Service._Service.SetDefault("http://localhost:5270/");
    }


}