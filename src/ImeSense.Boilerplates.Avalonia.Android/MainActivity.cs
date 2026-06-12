using System;

using Android.App;
using Android.Content.PM;

using Avalonia;
using Avalonia.Android;

using ImeSense.Boilerplates.Avalonia.Views;
using ImeSense.Boilerplates.Avalonia.ViewsModels;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ImeSense.Boilerplates.Avalonia.Android;

[Activity(
    Label = "ImeSense.Boilerplates.Avalonia.Android",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges =
        ConfigChanges.Orientation |
        ConfigChanges.ScreenSize |
        ConfigChanges.UiMode
)]
public class MainActivity : AvaloniaMainActivity<App>
{
    private IHost? _host;
    private bool _isHostDisposed;
    private App? _app;

    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        string basePath = AppContext.BaseDirectory;

        _host = Host.CreateDefaultBuilder()
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
                services.AddSingleton<MainView>();
                services.AddSingleton<MainViewModel>();
                services.AddLogging();
            })
            .Build();

        return base.CustomizeAppBuilder(builder)
            .AfterSetup(builder =>
            {
                if (builder.Instance is App app)
                {
                    _app = app;
                    app.Services = _host.Services;

                    _host.Start();
                }
            })
            .WithInterFont();
    }

    protected override void OnStop()
    {
        if (!_isHostDisposed && _host != null)
        {
            _host.StopAsync().GetAwaiter().GetResult();
            _host.Dispose();
            _isHostDisposed = true;
        }
        base.OnStop();
    }

    protected override void OnRestart()
    {
        if (_isHostDisposed && _app != null)
        {
            string basePath = AppContext.BaseDirectory;

            _host = Host.CreateDefaultBuilder()
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
                    services.AddSingleton<MainView>();
                    services.AddSingleton<MainViewModel>();
                    services.AddLogging();
                })
                .Build();

            _app.Services = _host.Services;

            _host.Start();

            _isHostDisposed = false;
        }
        base.OnRestart();
    }

    protected override void OnDestroy()
    {
        if (!_isHostDisposed && _host != null)
        {
            _host.StopAsync().GetAwaiter().GetResult();
            _host.Dispose();
        }
        base.OnDestroy();
    }
}
