using Avalonia;

using ImeSense.Boilerplates.Avalonia.Views;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ImeSense.Boilerplates.Avalonia.macOS;

internal class Program
{
    /// <summary>
    /// Initialization code.
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        using var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddSingleton<MainWindow>();
            })
            .Build();

        BuildAvaloniaApp()
            .AfterSetup(builder =>
            {
                if (builder.Instance is App app)
                {
                    app.Services = host.Services;

                    host.Start();
                }
            })
            .StartWithClassicDesktopLifetime(args);

        host.StopAsync().GetAwaiter().GetResult();
    }

    /// <summary>
    /// Avalonia configuration. Also used by visual designer.
    /// </summary>
    /// <returns></returns>
    public static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
    }
}
