using APISample.Core.ApplicationService;
using APISample.Infra.Data.Ef.SQL;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProductDbContext>();
builder.Services.AddScoped<ProductsService>();
var app = builder.Build();

app.MapGet("/", async(HttpContext context,ProductsService service) =>
{
    var products = service.GetProdcuts();
    string result = System.Text.Json.JsonSerializer.Serialize(products);
    context.Response.ContentType = "application/json";
    context.Response.StatusCode = 200;

    await context.Response.WriteAsync(result);

});

app.Run();
