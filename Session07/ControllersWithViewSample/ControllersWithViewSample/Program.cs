using ControllersWithViewSample.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<PersonDbContext>
    (c => c.UseSqlServer(builder.Configuration.GetConnectionString("PersonCnn")));
var app = builder.Build();
app.UseStaticFiles();
app.MapControllerRoute("Default", "{controller=People}/{action=GetAll}/{Id?}");


app.Run();
