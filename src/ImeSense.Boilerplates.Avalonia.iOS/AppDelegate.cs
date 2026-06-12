using System;

using Avalonia;
using Avalonia.iOS;

using Foundation;

using ImeSense.Boilerplates.Avalonia.Views;
using ImeSense.Boilerplates.Avalonia.ViewsModels;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using UIKit;

namespace ImeSense.Boilerplates.Avalonia.iOS;

/// <summary>
/// Responsible for launching UI of the application, as well as listening
/// (and optionally responding) to application events from iOS.
/// </summary>
[Register("AppDelegate")]
public partial class AppDelegate : AvaloniaAppDelegate<App>
{
    private IHost? _host;
    private NSObject? _enterBackgroundObserver;
    private NSObject? _enterForegroundObserver;
    private App? _app;

    public AppDelegate()
    {
    }

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

                    SetupNotifications();
                }
            })
            .WithInterFont();
    }

    private void SetupNotifications()
    {
        _enterBackgroundObserver = NSNotificationCenter.DefaultCenter.AddObserver(
            UIApplication.DidEnterBackgroundNotification,
            OnEnteredBackground
        );

        _enterForegroundObserver = NSNotificationCenter.DefaultCenter.AddObserver(
            UIApplication.WillEnterForegroundNotification,
            OnEnteringForeground
        );
    }

    private void OnEnteredBackground(NSNotification notification)
    {
        if (_host != null)
        {
            _host.StopAsync().GetAwaiter().GetResult();
            _host.Dispose();
        }
    }

    private void OnEnteringForeground(NSNotification notification)
    {
        if (_host == null || _app == null)
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

            if (_app != null)
            {
                _app.Services = _host.Services;

                _host.Start();
            }
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (_enterBackgroundObserver != null)
        {
            NSNotificationCenter.DefaultCenter.RemoveObserver(_enterBackgroundObserver);
            _enterBackgroundObserver = null;
        }

        if (_enterForegroundObserver != null)
        {
            NSNotificationCenter.DefaultCenter.RemoveObserver(_enterForegroundObserver);
            _enterForegroundObserver = null;
        }

        _host?.Dispose();

        base.Dispose(disposing);
    }
}
