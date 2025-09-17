using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using ShortUrl.CodeGenerator.API.DAL;
using ShortUrl.CodeGenerator.API.Services;

var builder = WebApplication.CreateBuilder(args);

// SQL Server
builder.Services.AddDbContext<CodeDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("CodeDb")));

// DI
builder.Services.AddScoped<ICodeRepository, CodeRepository>();
builder.Services.AddScoped<ICodeGenerationService, CodeGenerationService>();
builder.Services.AddScoped<IAllocationTracker, AllocationTracker>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();
app.MapControllers();

app.Run();