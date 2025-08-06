var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/cookies", async(HttpContext context) => {
    int Num01,Num02 = 0;
    string key = nameof(Num01);
    string key2 = nameof(Num02);
    Num01 = int.Parse(context.Request.Cookies[key] ?? "0");
    Num02 = int.Parse(context.Request.Cookies[key2] ?? "0");
    Num01++;
    Num02++;

    context.Response.Cookies.Append(key,Num01.ToString());
    context.Response.Cookies.Append(key2,Num02.ToString(),new CookieOptions
    {
        Path = "/cookies"
    });


    context.Response.StatusCode = 200;
    context.Response.ContentType = "text/html";
    await context.Response.WriteAsync($"<h1>{Num01}-{Num02}</h1>");

});
app.MapGet("/", () => "Hello World!");

app.Run();
