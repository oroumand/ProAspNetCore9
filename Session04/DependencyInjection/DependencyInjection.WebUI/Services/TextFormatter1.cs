namespace DependencyInjection.WebUI.Services;

public class TextFormatter1 : IResponseFormatter
{
    private static TextFormatter _instance;//= new TextFormatter();


    //public static TextFormatter Instance
    //{
    //    get
    //    {
    //        return _instance ;
    //    }
    //}
    //public static TextFormatter Instance
    //{
    //    get
    //    {
    //        return _instance ?? (_instance = new TextFormatter());
    //    }
    //}
    private int responseCounter = 0;
    public async Task Fromat(HttpContext context, string content)
    {
        responseCounter++;
        await context.Response.WriteAsync($"Count:{responseCounter}\r\n body:{content}");
    }
}
