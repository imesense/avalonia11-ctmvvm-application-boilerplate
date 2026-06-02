using System;

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;

using ImeSense.Boilerplates.Avalonia.Views;

using Microsoft.Extensions.DependencyInjection;

namespace ImeSense.Boilerplates.Avalonia;

public partial class App : Application
{
    public IServiceProvider? Services { get; set; }

    public override void Initialize() =>
        AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        // Remove Avalonia data validation to avoid duplication validations
        // from both Avalonia and CommunityToolkit.Mvvm.
        BindingPlugins.DataValidators.RemoveAt(0);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
        {
            desktopLifetime.MainWindow = Services?.GetRequiredService<MainWindow>();
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewLifetime)
        {
            singleViewLifetime.MainView = Services?.GetRequiredService<MainView>();
        }
    }
}
