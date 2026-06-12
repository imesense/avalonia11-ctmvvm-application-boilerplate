using System;

using Avalonia;

using ImeSense.Boilerplates.Avalonia.Views;
using ImeSense.Boilerplates.Avalonia.ViewsModels;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ImeSense.Boilerplates.Avalonia.macOS;

internal class Program
{
    /// <summary>
    /// Initialization code.
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        string basePath = AppContext.BaseDirectory;

        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(config =>
            {
                config.SetBasePath(basePath);
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                logging.AddDebug();
            })
            .ConfigureServices(services =>
            {
                services.AddSingleton<MainWindow>();
                services.AddSingleton<MainViewModel>();
                services.AddLogging();
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
