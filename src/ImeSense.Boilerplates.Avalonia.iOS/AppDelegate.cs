using Avalonia;
using Avalonia.iOS;

using Foundation;

namespace ImeSense.Boilerplates.Avalonia.iOS;

/// <summary>
/// Responsible for launching UI of the application, as well as listening
/// (and optionally responding) to application events from iOS.
/// </summary>
[Register("AppDelegate")]
public partial class AppDelegate : AvaloniaAppDelegate<App>
{
    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        return base.CustomizeAppBuilder(builder)
            .WithInterFont();
    }
}
