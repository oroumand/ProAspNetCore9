using APISample.Core.ApplicationService;
using APISample.Infra.Data.Ef.SQL;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProductDbContext>();
builder.Services.AddScoped<ProductsService>();
builder.Services.AddControllers();//.AddXmlDataContractSerializerFormatters();


builder.Services.Configure<MvcOptions>(opts => {
    opts.RespectBrowserAcceptHeader = true;
    opts.ReturnHttpNotAcceptable = true;
});


builder.Services.Configure<JsonOptions>(opts =>
{
    opts.JsonSerializerOptions.DefaultIgnoreCondition
    = JsonIgnoreCondition.WhenWritingNull;
});

var app = builder.Build();

app.MapControllers();

app.Run();
