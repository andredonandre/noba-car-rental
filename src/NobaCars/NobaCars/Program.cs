using BlazorBlueprint.Components;
using BlazorBlueprint.Primitives.Extensions;
using NobaCars.Components;
using NobaCars.Core.Interfaces;
using NobaCars.Core.Services;
using NobaCars.Infra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddJsonFileStorage("database.json");
builder.Services.AddSingleton<IBookingService, BookingService>();
builder.Services.AddSingleton<IInventoryService, InventoryService>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add BlazorBlueprint services (primitives, toast, dialog)
builder.Services.AddBlazorBlueprintComponents();
builder.Services.AddBlazorBlueprintPrimitives();

var app = builder.Build();

await app.Services.SeedDataAsync();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
