using System.Threading.Tasks;

using Avalonia;
using Avalonia.Browser;

using ImeSense.Boilerplates.Avalonia;
using ImeSense.Boilerplates.Avalonia.Views;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal sealed partial class Program
{
    private static async Task Main(string[] args)
    {
        using var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddSingleton<MainView>();
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
