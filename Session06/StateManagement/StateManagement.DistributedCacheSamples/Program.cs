using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDistributedMemoryCache();
builder.Services.AddDistributedSqlServerCache(c =>
{
    c.SchemaName = "dbo";
    c.TableName = "CahceData";
    c.ConnectionString = "Server=172.30.176.1;Database=DistCache; User Id=sa; Password=1qaz!QAZ; TrustServerCertificate=True";
});
builder.Services.AddSession(c =>
{
    c.IdleTimeout = TimeSpan.FromMinutes(10);
    c.Cookie.IsEssential = true;
});


var app = builder.Build();
app.UseSession();
app.MapGet("/dcache", async (HttpContext context, IDistributedCache dCache) =>
{
    int Num01 = 0;
    string key = nameof(Num01);

    Num01 = int.Parse(dCache.GetString(key) ?? "0");
    Num01++;
    dCache.SetString(key, Num01.ToString(), new DistributedCacheEntryOptions
    {
        AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10),
        SlidingExpiration = TimeSpan.FromMinutes(10)
    });
    context.Response.StatusCode = 200;
    context.Response.ContentType = "text/html";
    await context.Response.WriteAsync($"<h1>{Num01}</h1>");

});

app.MapGet("/session", async (HttpContext context) =>
{
    int Num01 = 0;
    string key = nameof(Num01);
    
    Num01 = context.Session.GetInt32(key) ?? 0;
    Num01++;
    context.Session.SetInt32(key, Num01);
    context.Response.StatusCode = 200;
    context.Response.ContentType = "text/html";
    await context.Response.WriteAsync($"<h1>{Num01}</h1>");

});

app.MapGet("/sessionId", async (HttpContext context) =>
{    
    context.Response.StatusCode = 200;
    context.Response.ContentType = "text/html";
    await context.Response.WriteAsync($"<h1>{context.Session.Id}</h1>");

});
app.MapGet("/", () => "Hello World!");

app.Run();
