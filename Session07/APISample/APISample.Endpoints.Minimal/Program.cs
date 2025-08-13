using APISample.Core.ApplicationService;
using APISample.Infra.Data.Ef.SQL;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProductDbContext>();
builder.Services.AddScoped<ProductsService>();
var app = builder.Build();
app.MapGet("/", async (HttpContext context, [FromServices] ProductsService service) =>
{
    var products = service.GetProdcuts();
    string result = System.Text.Json.JsonSerializer.Serialize(products);
    return Results.Ok(result);

});
app.Run();
