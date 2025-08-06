using StateManagement.CacheAdapter.Patterns;

var builder = WebApplication.CreateBuilder(args);

if (builder.Configuration["CacheType"]== "1")
{
    builder.Services.AddNikamoozDis();

}
else
{
    builder.Services.AddNikamoozMemory();

}


var app = builder.Build();

app.MapGet("/nikamoozCahce", (INikamoozCahce cache) =>
{
    cache.Set("", "");
    int a = cache.Get<int>("");
});
app.MapGet("/", () => "Hello World!");

app.Run();
