# Supporting interactive rendering modes at the per page/component level with the new .NET MAUI Blazor Hybrid and Web solution template

This sample demonstrates how you can implement a class to intercept any render modes that are specified in the page/component level in your shared razor class libraries when sharing UI across Blazor Hybrid and Blazor Web apps. Blazor Hybrid apps always run interactively and do not support ASP.NET rendering modes. 

To recreate this solution yourself:

1. Use the new `maui-blazor-web` solution template in .NET 9 to create your .NET MAUI Blazor Hybrid and Web solution with a shared Razor Class Library for the UI automatically. Select the interactivity to `InteractiveAuto` render mode. 
2. Open the `.Web` project's `Components\App.razor` file and delete the `@rendermode="InteractiveAuto"` from both the `HeadOutlet` and `Routes` tags.
3. Open the `.Shared` project's `Pages\Counter.razor` page and add `@rendermode InteractiveAuto` at the top of the file.
4. At this point if you tried to run the MAUI app, it would throw an error through the WebView that rendermode is not supported.
5. Add the following class to the `.Shared` Razor Class Library project:
```csharp
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorHybridWebDemo.Shared //change this to your project's Name.Shared
{
    // Set the interactive render modes for use by components in the Shared class library which can be overridden by the MAUI client.
    public static class InteractiveRenderSettings
    {
        public static IComponentRenderMode? InteractiveServer { get; set; } = RenderMode.InteractiveServer;
        public static IComponentRenderMode? InteractiveAuto { get; set; } = RenderMode.InteractiveAuto;
        public static IComponentRenderMode? InteractiveWebAssembly { get; set; } = RenderMode.InteractiveWebAssembly;

        public static void ConfigureBlazorHybridRenderModes()
        {
            InteractiveServer = null;
            InteractiveAuto = null;
            InteractiveWebAssembly = null;
        }
    }
}
```
6. In the `.Shared` project's `_Imports.razor` file change the line `@using static Microsoft.AspNetCore.Components.Web.RenderMode` to `@using static InteractiveRenderSettings`
7. In the MAUI project, add a call in the `MauiProgram.cs` add a call to set the rendering mode to `null`:
```csharp
 public static class MauiProgram
 {
     public static MauiApp CreateMauiApp()
     {
         // Set any per page/component render modes used by components in the Shared class library to Null.
         // MAUI apps are always interactive and Blazor rendering modes are not supported.
         // In this example the Counter.razor page has its render mode set to InteractiveAuto.
         InteractiveRenderSettings.ConfigureBlazorHybridRenderModes();            
```
8. Run the MAUI app and it will now work. The Web app is unaffected. 
