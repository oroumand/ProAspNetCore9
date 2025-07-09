namespace RoutingSamples.Middlewares;

public class PopulationMiddleware
{
    private readonly RequestDelegate _next;
    public PopulationMiddleware()
    {

    }
    public PopulationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        //var segements = context.Request.Path.ToString().Split('/', StringSplitOptions.RemoveEmptyEntries);
        //if (segements.Length == 2 )//&& segements[0] == "population")
        //{

        string city = context.Request.RouteValues["city"].ToString();

        int? pop = null;
        switch (city)
        {
            case "Tehran":
                pop = 12_000_000;
                break;
            case "Telaviv":
                pop = 1_000_000;
                break;
            default:
                break;
        }
        if (pop.HasValue)
        {
            await context.Response.WriteAsync($"population Name: {pop}");
        }
        if (_next != null)
        {

            await _next(context);
        }
    }


}