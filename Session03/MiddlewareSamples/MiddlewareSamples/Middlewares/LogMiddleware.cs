using MiddlewareSamples.Database;

namespace MiddlewareSamples.Middlewares;

public class LogMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public LogMiddleware(RequestDelegate next, ILogger logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, PersonDbContext personDbContext)
    {
        Console.WriteLine($"Request start at : {DateTime.Now.Ticks}");
        _logger.LogInformation("Request start at : {DateTime}", DateTime.Now.Ticks);
        await _next(context);
        _logger.LogInformation("Request End at : {DateTime}", DateTime.Now.Ticks);

        Console.WriteLine($"Request Finished at : {DateTime.Now.Ticks}");

    }
}


public class MyTaskMiddleware
{
    private readonly RequestDelegate _next;

    public MyTaskMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {

        await Task.Delay(2000);
        await _next(context);


    }
}

public static class MyMiddlewareRegistrator
{
    public static IApplicationBuilder UseMyLog(this IApplicationBuilder app)
    {
        app.UseMiddleware<MyTaskMiddleware>();

        app.UseMiddleware<LogMiddleware>();
        return app;
    }
}

public class MyFactoryBase : IMiddleware
{
    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        throw new NotImplementedException();
    }
}