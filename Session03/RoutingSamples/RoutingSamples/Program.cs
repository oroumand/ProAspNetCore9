using Microsoft.AspNetCore.Http;
using RoutingSamples;
using RoutingSamples.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RouteOptions>(opts =>
{
    opts.ConstraintMap.Add("nationalCode",
    typeof(SSNConstraint));
});

var app = builder.Build();
//app.UseDeveloperExceptionPage();
app.UseExceptionHandler();
app.UseRouting();

app.Use(async (context, next) =>
{
    throw new Exception();
    Endpoint? end = context.GetEndpoint();
    if (end != null)
    { await context.Response.WriteAsync($"{end.DisplayName} Selected \n"); }
    else { await context.Response.WriteAsync("No Endpoint Selected \n"); }
    await next();
});
//app.Map("{number:int}", async context =>
//{
//    await context.Response.WriteAsync("Routed to the int endpoint");
//}).WithDisplayName("Int Endpoint").Add(b => ((RouteEndpointBuilder)b).Order = 1);
//app.Map("{number:double}", async context =>
//{
//    await context.Response.WriteAsync("Routed to the double endpoint");
//}).WithDisplayName("Double Endpoint").Add(b => ((RouteEndpointBuilder)b).Order = 2);


//app.MapGet("{controller:int}", async context =>
//{
//    await context.Response.WriteAsync("For int Value\r\n");
//    foreach (var item in context.Request.RouteValues)
//    {
//        await context.Response.WriteAsync($"Key:{item.Key},Value:{item.Value}\r\n");
//    }
//}).Add(c => ((RouteEndpointBuilder)c).Order = 1);
//app.MapGet("{controller:double}", async context =>
//{
//    await context.Response.WriteAsync("For double Value\r\n");
//    foreach (var item in context.Request.RouteValues)
//    {
//        await context.Response.WriteAsync($"Key:{item.Key},Value:{item.Value}\r\n");
//    }
//}).Add(c => ((RouteEndpointBuilder)c).Order = 2);
//app.MapGet("{controller=Home}/{action=Index}/{id:nationalCode}/", async context =>
//{
//    await context.Response.WriteAsync("For int Value\r\n");
//    foreach (var item in context.Request.RouteValues)
//    {
//        await context.Response.WriteAsync($"Key:{item.Key},Value:{item.Value}\r\n");
//    }
//});

//app.MapGet("{controller=Home}/{action=Index}/{id:alpha}/", async context =>
//{
//    await context.Response.WriteAsync("For string Value\r\n");
//    foreach (var item in context.Request.RouteValues)
//    {
//        await context.Response.WriteAsync($"Key:{item.Key},Value:{item.Value}\r\n");
//    }
//});
//app.MapGet("{controller=Home}/{action=Index}/{id?}/{*catchAll}", async context =>
//    {
//        foreach (var item in context.Request.RouteValues)
//        {
//            await context.Response.WriteAsync($"Key:{item.Key},Value:{item.Value}\r\n");
//        }
//        await context.Response.WriteAsync("Request Was Routed");
//    });
//app.UseMiddleware<CapitalMiddleware>();
//app.UseMiddleware<PopulationMiddleware>();
//app.UseEndpoints(endpoints =>
//{
//app.MapGet("routing", async context =>
//    {
//        await context.Response.WriteAsync("Request Was Routed");
//    });
//app.MapGet("file/{filename}.{extentsion}", async context =>
//{
//    foreach (var item in context.Request.RouteValues)
//    {
//        await context.Response.WriteAsync($"Key:{item.Key},Value:{item.Value}\r\n");
//    }
//    await context.Response.WriteAsync("Request Was Routed");
//});
//app.MapGet("routing/{Var1}/{Var2}", async context =>
//    {
//        foreach (var item in context.Request.RouteValues)
//        {
//            await context.Response.WriteAsync($"Key:{item.Key},Value:{item.Value}\r\n");
//        }
//        await context.Response.WriteAsync("Request Was Routed");
//    });
//app.MapGet("Capital/{country}", new CapitalMiddleware().InvokeAsync);
//app.MapGet("Population/AndNewValue/AndTheOthers/{city}", new PopulationMiddleware().InvokeAsync)
//    .WithMetadata(new RouteNameMetadata("Pop"));
//});

//app.Run(async (context) =>
//{
//    await context.Response.WriteAsync("Not Fount");
//});
app.Run();
