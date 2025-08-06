using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMemoryCache();
var app = builder.Build();
app.MapGet("/memorycache", async (HttpContext context, IMemoryCache memoryCache) =>
{
    int Num01 = 0;
    string key = nameof(Num01);
    Num01 = memoryCache.Get<int>(key);
    Num01++;
    memoryCache.Set(key, Num01);
    context.Response.StatusCode = 200;
    context.Response.ContentType = "text/html";
    await context.Response.WriteAsync($"<h1>{Num01}</h1>");

});
app.MapGet("/", () => "Hello World!");

app.Run();
