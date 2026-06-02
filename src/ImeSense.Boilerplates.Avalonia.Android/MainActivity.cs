using Android.App;
using Android.Content.PM;

using Avalonia;
using Avalonia.Android;

using ImeSense.Boilerplates.Avalonia.Views;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddSingleton<MainView>();
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
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<MainView>();
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
