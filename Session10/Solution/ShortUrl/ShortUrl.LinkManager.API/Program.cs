using Microsoft.EntityFrameworkCore;
using Polly;
using Polly;
using Polly.Extensions.Http;
using Polly.Extensions.Http;
using Scalar.AspNetCore;
using ShortUrl.LinkManager.API.Clients;
using ShortUrl.LinkManager.API.DAL;
using ShortUrl.LinkManager.API.Options;
using ShortUrl.LinkManager.API.Services;
using StackExchange.Redis;
using StackExchange.Redis;
var builder = WebApplication.CreateBuilder(args);

// ===== Options =====
builder.Services.Configure<ShortenerOptions>(builder.Configuration.GetSection("Shortener"));
builder.Services.Configure<RedisOptions>(builder.Configuration.GetSection("Redis"));
builder.Services.Configure<CodeGenOptions>(builder.Configuration.GetSection("CodeGenerator"));

// ===== DB =====
builder.Services.AddDbContext<LinkDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("LinkDb")));

// ===== Redis =====
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var opt = sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<RedisOptions>>().Value;
    return ConnectionMultiplexer.Connect(opt.ConnectionString);
});

// ===== HttpClient (Code Generator) + Polly =====
static IAsyncPolicy<HttpResponseMessage> RetryPolicy() =>
    HttpPolicyExtensions
        .HandleTransientHttpError()
        .WaitAndRetryAsync(new[] { TimeSpan.FromMilliseconds(200), TimeSpan.FromMilliseconds(500), TimeSpan.FromSeconds(1) });

builder.Services.AddHttpClient<ICodeGeneratorClient, CodeGeneratorClient>();
    //.AddPolicyHandler(RetryPolicy());

// ===== DI =====
builder.Services.AddScoped<ILinkRepository, LinkRepository>();
builder.Services.AddScoped<ICacheManager, CacheManager>();
builder.Services.AddScoped<ILinkService, LinkService>();
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IRedirectResolver, RedirectResolver>();


builder.Services.AddScoped<IClickEventRepository, ClickEventRepository>();
builder.Services.AddScoped<IClickLogger, ClickLogger>();
var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();

app.MapControllers();

app.Run();
