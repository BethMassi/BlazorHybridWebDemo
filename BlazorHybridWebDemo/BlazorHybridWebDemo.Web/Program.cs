using BlazorHybridWebDemo.Shared.Services;
using BlazorHybridWebDemo.Web.Components;
using BlazorHybridWebDemo.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Add device-specific services used by the BlazorHybridWebDemo.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(
        typeof(BlazorHybridWebDemo.Shared._Imports).Assembly,
        typeof(BlazorHybridWebDemo.Web.Client._Imports).Assembly);

app.Run();