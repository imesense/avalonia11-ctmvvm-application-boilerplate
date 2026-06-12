using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.Extensions.Logging;

namespace ImeSense.Boilerplates.Avalonia.ViewsModels;

public class MainViewModel : ObservableObject
{
    private readonly ILogger _logger;

    public MainViewModel(ILogger<MainViewModel> logger)
    {
        _logger = logger;
        _logger.LogInformation("MainViewModel initialized");
    }
}
