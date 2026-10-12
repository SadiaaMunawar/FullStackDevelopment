using ProductManagmentApp.Components;
using ProductManagmentApp.Components;
using ProductManagementApp.BusinessLayer.Interfaces;
using ProductManagementApp.BusinessLayer.Services;
using ProductManagementApp.DataAccessLayer.Interfaces;
using ProductManagementApp.DataAccessLayer.Repositories;
using ProductManagementApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add Razor components and Interactive Server mode
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register 3-Tier Layer Dependencies
builder.Services.AddSingleton<IProductRepository, ProductRepositoryJson>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();