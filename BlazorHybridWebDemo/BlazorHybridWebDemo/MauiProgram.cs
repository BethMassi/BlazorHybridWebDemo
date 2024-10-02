using BlazorHybridWebDemo.Services;
using BlazorHybridWebDemo.Shared;
using BlazorHybridWebDemo.Shared.Services;
using Microsoft.Extensions.Logging;

namespace BlazorHybridWebDemo
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            // Set any per page/component render modes used by components in the Shared class library to Null.
            // MAUI apps are always interactive and Blazor rendering modes are not supported.
            // In this example the Counter.razor page has its render mode set to InteractiveAuto.
            InteractiveRenderSettings.ConfigureBlazorHybridRenderModes();   

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Add device-specific services used by the BlazorHybridWebDemo.Shared project
            builder.Services.AddSingleton<IFormFactor, FormFactor>();

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
