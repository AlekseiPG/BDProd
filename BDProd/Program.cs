using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BDProd.Data;
using BDProd.Services;
using Microsoft.Extensions.FileProviders;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BDProdContextConnection") ?? throw new InvalidOperationException("Connection string 'BDProdContextConnection' not found.");

builder.Services.AddDbContext<BDProdContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<BDProdContext>();
builder.Services.AddScoped<IPasswordHasher<IdentityUser>, PlainTextPasswordHasher<IdentityUser>>();
builder.Services.AddScoped<IImageService, ImageService>();


// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.Run();
