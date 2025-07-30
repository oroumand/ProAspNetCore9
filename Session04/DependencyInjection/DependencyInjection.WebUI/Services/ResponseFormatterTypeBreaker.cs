namespace DependencyInjection.WebUI.Services;

public class ResponseFormatterTypeBreaker
{
    public static IResponseFormatter GetResponseFormatter()
    {
        return new HtmlFormatter();
    }
}


//public class ResponseFormatterFactory
//{
//    public static IResponseFormatter GetResponseFormatter(FormmaterType formmaterType = FormmaterType.Text)
//    {
//        if (formmaterType == FormmaterType.Html)
//            return new HtmlFormatter();
//        else return new TextFormatter();
//    }
//}
