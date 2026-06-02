using Avalonia;
using Avalonia.iOS;

using Foundation;

using ImeSense.Boilerplates.Avalonia.Views;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddSingleton<MainView>();
            })
            .Build();

        var result = base.CustomizeAppBuilder(builder)
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

        return result;
    }

    private void SetupNotifications()
    {
        _enterBackgroundObserver = NSNotificationCenter.DefaultCenter.AddObserver(
            UIApplication.DidEnterBackgroundNotification,
            OnEnteredBackground);

        _enterForegroundObserver = NSNotificationCenter.DefaultCenter.AddObserver(
            UIApplication.WillEnterForegroundNotification,
            OnEnteringForeground);
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
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<MainView>();
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
