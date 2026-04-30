using System.Threading.Tasks;

using Avalonia;
using Avalonia.Browser;

using ImeSense.Boilerplates.Avalonia;

internal sealed partial class Program
{
    private static Task Main(string[] args)
    {
        return BuildAvaloniaApp()
            .WithInterFont()
            .StartBrowserAppAsync("out");
    }

    public static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder.Configure<App>();
    }
}
