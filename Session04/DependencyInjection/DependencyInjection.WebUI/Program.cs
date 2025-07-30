using DependencyInjection.WebUI.Middlewares;
using DependencyInjection.WebUI.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTextFormatter();

builder.Services.AddKeyedScoped<IResponseFormatter, TextFormatter>("Text");
builder.Services.AddKeyedScoped<IResponseFormatter, HtmlFormatter>("Html");
builder.Services.AddTransient(typeof(ICollection<>), typeof(List<>));

var app = builder.Build();
app.UseMiddleware<WeatherMiddeware>();
app.MapGet("/weather/endpoint", WeatherEndpoint.Endpoint);

//IResponseFormatter ResponseFormatter = ResponseFormatterFactory.GetResponseFormatter(FormmaterType.Html);
//app.MapGet("/weather/function", async (HttpContext context,IResponseFormatter ResponseFormatter) =>
//{
//    await ResponseFormatter.Fromat(context, "It is rainy in Rasht");

//});

app.Run();
