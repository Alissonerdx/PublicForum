using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using PublicForum.Application.AutoMapper;
using PublicForum.Application.Interfaces;
using PublicForum.Application.Services;
using PublicForum.Auth.Interface;
using PublicForum.Auth.Model;
using PublicForum.Domain.Interfaces.Repositories;
using PublicForum.Repository;
using PublicForum.Repository.Interfaces;
using PublicForum.Repository.Repositories;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<EFContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<EFContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(150);
    options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
    options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
    options.AccessDeniedPath = "/AcessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
    options.SlidingExpiration = true;
    options.Cookie.Name = "PublicForumCookie";
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
});

//UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//IOF Services
builder.Services.AddScoped<ITopicService, TopicService>();

//IOF Repositories
builder.Services.AddScoped<ITopicRepository, TopicRepository>();

//AutoMapper Settings
builder.Services.AddAutoMapperConfiguration();

//Resolver
builder.Services.AddScoped<IUserResolver, UserResolver>();




builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("en-US");
});

var app = builder.Build();

var supportedCultures = new[] { new CultureInfo("en-US") };

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

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
    pattern: "{controller=Topic}/{action=Index}/{id?}");

app.Run();
