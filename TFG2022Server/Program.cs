using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor;
using TFG2022Server.Data;
using TFG2022Server.Services;
using TFG2022Server.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("TFG2022Connection")
    ?? throw new InvalidOperationException("Connection 'TFG2022' not found");

// Add services to the container.
builder.Services.AddDbContext<TFG2022Context>(options => options.UseSqlServer(connectionString));

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddSyncfusionBlazor();

builder.Services.AddScoped<IUsuarioManagementService, UsuarioManagementService>();
builder.Services.AddScoped<IClienteManagementService, ClienteManagementService>();

var app = builder.Build();
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt/QHJqVVhjWlpFdEBBXHxAd1p/VWJYdVt5flBPcDwsT3RfQF9jQX5bd0diXHxXcHNVRA==;Mgo+DSMBPh8sVXJ0S0R+XE9HcFRDX3xKf0x/TGpQb19xflBPallYVBYiSV9jS3xTfkVrWH1fdXdcRmheUw==;Mgo+DSMBMAY9C3t2VVhiQlFadVlJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxRdk1jUX5ccXFWT2BVVUY=;NjkzMTkxQDMyMzAyZTMyMmUzMGVUWk5qaEJUVVIzYmN5WWphTXE1V3RrUGtQUURmRVZHTnN5QzJSOUxsNDg9");


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
