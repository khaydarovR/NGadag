using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using NGadag.Models;
using RRshop.DTO;
using Microsoft.Extensions.DependencyInjection;

System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
Console.WriteLine("Start 0");

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ngadag>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ngadag") ?? throw new InvalidOperationException("Connection string 'ngadag' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
    AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, (conf) => conf.LoginPath = "/Account/Login");
builder.Services.AddAuthorization();

builder.Services.AddDbContext<ngadagContext>(options =>
options.UseMySql(builder.Configuration.GetConnectionString("MySql"), ServerVersion.Parse("8.0.32-mysql")));

builder.Services.AddAutoMapper(typeof(UserMapping));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

//Scaffold-DbContext "Server=localhost; Port=3306; Database=ngadag; Uid=root; Pwd=root" Pomelo.EntityFrameworkCore.MySql -OutputDir Models -f

//dotnet aspnet-codegenerator controller -m Category -dc MyDbContext -rrshopContext CategoriesController -async