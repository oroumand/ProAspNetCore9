using DependencyInjection.WebUI.Services;

namespace DependencyInjection.WebUI.Middlewares;

public class WeatherMiddeware
{
    private readonly RequestDelegate _next;

    public WeatherMiddeware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context,
                             TextFormatter responseFormatter1,
                             HtmlFormatter responseFormatter2,
                             [FromKeyedServices("Text")] IResponseFormatter responseFormatter3,
                             [FromKeyedServices("Html")] IResponseFormatter responseFormatter4,

                             ICollection<string> mystring,
                             ICollection<int> myint,
                             ICollection<IResponseFormatter> responseFormatters)
    {
        if (context.Request.Path == "/middleware/classs")
        {
            await responseFormatter1.Fromat(context, "Middleware class: It is Raining In tehran 1 \r\n");
            await responseFormatter2.Fromat(context, "Middleware class: It is Raining In tehran 2");
        }
        else
        {
            await _next(context);
        }
    }
}
