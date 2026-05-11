using Avalonia;
using Avalonia.Tizen;

namespace ImeSense.Boilerplates.Avalonia.Tizen;

internal class Program : NuiTizenApplication<App>
{
    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        return base
            .CustomizeAppBuilder(builder)
            .UseTizen()
            .WithInterFont();
    }

    private static void Main(string[] args)
    {
        var app = new Program();
        app.Run(args);
    }
}
