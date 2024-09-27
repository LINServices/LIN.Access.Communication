namespace LIN.Access.Communication;

public class Build
{
    public static void Init()
    {
        Service._Service = new();
        Service._Service.SetDefault("https://api.communication.linplatform.com/");
        //Service._Service.SetDefault("http://localhost:5270/");
    }
}