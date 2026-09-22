using Microsoft.EntityFrameworkCore;
using reactSyncfusionApp.Models;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;

var builder = WebApplication.CreateBuilder(args);

//prevent CORS error to use React as front end - 1
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactApp", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

//register syncfusion license
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NxYtFisQPR08Cit/VkJ+Xk9GfV1CX2VUf1NrR2JJf1t6dVJMYFRaRnZdRF1qS39Tc0RkWXdYc3NXTWNY;Ngo9BigBOggjGyl/VkJ+Xk9GfV1CX2VUf1NrR2JJf1x6cVNMYlxaRnZdRF1qS39Tc0dhWHhZcnBWTWNY;Ix0oFS8QJAw9HSQvXkVkQlNadFRAXWFPY1J2WGFbb15yflVEal1WT3RfQFtjQHxRdkdjW35Zc3VXRWtfVQ==");

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SalesInvoiceDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

//prevent CORS error to use React as front end - 2
app.UseCors("ReactApp");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
