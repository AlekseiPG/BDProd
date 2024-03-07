using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BDProd.Data;
using BDProd.Services;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authorization;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BDProdContextConnection") ?? throw new InvalidOperationException("Connection string 'BDProdContextConnection' not found.");

builder.Services.AddDbContext<BDProdContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<BDProdContext>();
builder.Services.AddScoped<IPasswordHasher<IdentityUser>, PlainTextPasswordHasher<IdentityUser>>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowSpecificOrigins",
    builder =>
    {
        builder.WithOrigins("https://prodinfo.everys.com/externe/FindProd.ashx")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddDirectoryBrowser();

//builder.Services.AddAuthorization(options =>
//{
//    options.FallbackPolicy = new AuthorizationPolicyBuilder()
//        .RequireAuthenticatedUser()
//        .Build();
//});

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

//app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider("C:\\Changes_C\\Projects\\TESTING"),
//    RequestPath = "/TestingImages"
//});

//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(
//        Path.Combine(Directory.GetCurrentDirectory(), "C:\\Changes_C\\Projects\\TESTING")),
//    RequestPath = "/TESTING2"
//});

//app.UseFileServer(new FileServerOptions
//{
//    FileProvider = new PhysicalFileProvider("C:\\Changes_C\\Projects\\TESTING"),
//    RequestPath = "/TESTING2",
//    EnableDirectoryBrowsing = true
//});

//string customFolderPath = "C:\\Changes_C\\Projects\\TESTING";

//app.UseFileServer(new FileServerOptions
//{
//    FileProvider = new PhysicalFileProvider(Path.Combine(customFolderPath)),
//    RequestPath = "/TESTING2",
//    EnableDirectoryBrowsing = true
//});

app.UseCors("MyAllowSpecificOrigins");

app.Run();
