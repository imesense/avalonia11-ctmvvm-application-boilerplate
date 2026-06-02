using System.Threading.Tasks;

using Avalonia;

using ImeSense.Boilerplates.Avalonia.Views;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ImeSense.Boilerplates.Avalonia.Linux;

internal class Program
{
    /// <summary>
    /// Initialization code.
    /// </summary>
    /// <param name="args"></param>
    public static async Task Main(string[] args)
    {
        using var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddSingleton<MainWindow>();
            })
            .Build();

        BuildAvaloniaApp()
            .AfterSetup(async builder =>
            {
                if (builder.Instance is App app)
                {
                    app.Services = host.Services;

                    await host.StartAsync();
                }
            })
            .StartWithClassicDesktopLifetime(args);

        await host.StopAsync();
    }

    /// <summary>
    /// Avalonia configuration. Also used by visual designer.
    /// </summary>
    /// <returns></returns>
    public static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .UseX11()
            .WithInterFont()
            .LogToTrace();
    }
}
