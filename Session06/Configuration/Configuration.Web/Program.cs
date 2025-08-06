using Configuration.Web.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.Sources.Clear();
builder.Configuration.AddKeyPerFile("G:\\Config");
builder.Configuration.AddUserSecrets("2ba5efb9-9b4c-4ef0-be65-383732f5f00b");
builder.Configuration.AddEnvironmentVariables();
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, false)
    .AddJsonFile("appsettings.Development.json", true, false);
var providers = builder.Configuration.Sources;

var config = builder.Configuration;
//builder.Services.Configure<Customer>(builder.Configuration.GetSection("Customer"));


var opt = new Customer();
builder.Configuration.GetSection("Customer").Bind(opt);
builder.Services.AddSingleton(opt);


var app = builder.Build();

app.MapGet("/config", async (IConfiguration config, HttpContext context) =>
{
    string logLevelDefault = config["Logging:LogLevel:Default"];

    context.Response.StatusCode = 200;
    context.Response.ContentType = "text/html";
    await context.Response.WriteAsync($"<h1>{logLevelDefault}</h1>");
});
app.MapGet("/IOption", async (Customer customer, HttpContext context) =>
{


    context.Response.StatusCode = 200;
    context.Response.ContentType = "text/html";
    await context.Response.WriteAsync($"<h1>{customer.Name} - {customer.Age}</h1>");
});
app.MapGet("/", () => "Hello World!");

app.Run();
