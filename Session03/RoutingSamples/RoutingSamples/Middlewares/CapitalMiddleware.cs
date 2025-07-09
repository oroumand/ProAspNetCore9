namespace RoutingSamples.Middlewares;

public class CapitalMiddleware
{
    private readonly RequestDelegate _next;

    public CapitalMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public CapitalMiddleware()
    {
        
    }

    public async Task InvokeAsync(HttpContext context)
    {
        //var segements = context.Request.Path.ToString().Split('/', StringSplitOptions.RemoveEmptyEntries);
        //if (segements.Length == 2 )//&& segements[0] == "Capital")
        //{
            string country = context.Request.RouteValues["country"].ToString();
            string capitalName = string.Empty;
            switch (country)
            {
                case "Iran":
                    capitalName = "Tehran";
                    break;
                case "Israel":
                    capitalName = "Telaviv";
                    break;
                case "Tehran":
                case "Telaviv":
                LinkGenerator? generator =
      context.RequestServices.GetService<LinkGenerator>();
                string? url = generator?.GetPathByRouteValues(context,
                "Pop", new { city = country });
                if (url != null)
                {
                    context.Response.Redirect(url);
                }
                return;

                break;
                default:
                    break;
            }
            if (!string.IsNullOrEmpty(capitalName))
            {
                await context.Response.WriteAsync($"Capital Name: {capitalName}");
            }
           
       // }
        if (_next != null)
        {

            await _next(context);
        }
    }
}
