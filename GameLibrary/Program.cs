using GameLibrary.Core.Extensions;
using GameLibrary.Extensions;
using GameLibrary.Infrastructure.Data;
using GameLibrary.Infrastructure.Data.Entities;
using GameLibrary.ModelBinders;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//builder.Host.ConfigureServices((context, services) =>
//{
//    services.Configure<KestrelServerOptions>(
//        context.Configuration.GetSection("Kestrel"));
//});

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


//builder.Services.AddDistributedSqlServerCache(options =>
//{
//    options.ConnectionString = builder.Configuration.GetConnectionString(
//        "DefaultConnection");
//    options.SchemaName = "dbo";
//    options.TableName = "TestCache";
//});


builder.Services.AddDefaultIdentity<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireNonAlphanumeric = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();


//builder.Services.AddStackExchangeRedisCache(options =>
//{
//    options.Configuration = builder.Configuration.GetConnectionString("Redis");
//});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.HttpOnly = true;
    options.LoginPath = "/User/Login";
    options.LogoutPath = "/User/Logout";
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("NoAJAXRequests", builder => builder.WithOrigins("https://localhost:7189"));
});

builder.Services.AddControllersWithViews().AddMvcOptions(options =>
{
    options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
    options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
});

builder.Services.AddApplicationServices();
//builder.Services.AddResponseCaching();

var app = builder.Build();
app.SeedAdmin();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("NoAJAXRequests");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
      name: "gameDetails",
      pattern: "Game/Details/{gameId}/{information}"
    );

    app.MapRazorPages();
});
//app.UseResponseCaching();

app.Run();
