using System.Threading.Tasks;

using Avalonia;
using Avalonia.Browser;

using ImeSense.Boilerplates.Avalonia;
using ImeSense.Boilerplates.Avalonia.Views;
using ImeSense.Boilerplates.Avalonia.ViewsModels;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

internal sealed partial class Program
{
    private static async Task Main(string[] args)
    {
        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(config =>
            {
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddDebug();
            })
            .ConfigureServices(services =>
            {
                services.AddSingleton<MainView>();
                services.AddSingleton<MainViewModel>();
                services.AddLogging();
            })
            .Build();

        await BuildAvaloniaApp()
            .AfterSetup(async builder =>
            {
                if (builder.Instance is App app)
                {
                    app.Services = host.Services;

                    await host.StartAsync();
                }
            })
            .WithInterFont()
            .StartBrowserAppAsync("out");

        await host.StopAsync();
    }

    public static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder.Configure<App>();
    }
}
