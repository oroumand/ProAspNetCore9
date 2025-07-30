namespace DependencyInjection.WebUI.Middlewares;

public class WeatherEndpoint
{
    public static async Task Endpoint(HttpContext context)
    {
        await context.Response.WriteAsync("endpoint class: It is Raining In endpoint ");

    }
}