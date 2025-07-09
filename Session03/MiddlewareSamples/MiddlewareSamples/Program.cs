using MiddlewareSamples;
using MiddlewareSamples.Middlewares;
using MiddlewareSamples.Middlewares;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<MyFactoryBase>();
builder.Services.AddScoped<IMyService, MyService>();

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
app.UseStaticFiles();

app.UseWelcomePage();
app.UseMiddleware<MyFactoryBase>();
app.UseMyLog();

app.Use(async (context, next) =>
{
    var myservice1 = context.RequestServices.GetRequiredService<IMyService>();
    var myservice2 = context.RequestServices.GetRequiredService<IMyService>();

    var scop1 = context.RequestServices.CreateScope();
    var myservcie3 = scop1.ServiceProvider.GetRequiredService<IMyService>();


    myservice1.Add();
    myservice1.Add();
    myservice2.Add();
    myservcie3.Add();
    if (context.Request.Method == "GET")
    {

    }
    else
    {
        await next();

    }
});
app.Run();
