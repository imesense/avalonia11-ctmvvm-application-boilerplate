using System;

using Avalonia.Controls;
using Avalonia.Controls.Templates;

using CommunityToolkit.Mvvm.ComponentModel;

namespace ImeSense.Boilerplates.Avalonia.DataTemplates;

public class ViewDataTemplate : IDataTemplate
{
    public Control Build(object? data)
    {
        string name = data!.GetType().FullName!.Replace("ViewModel", "View");
        Type? type = Type.GetType(name);
        if (type is not null)
        {
            return (Control) Activator.CreateInstance(type)!;
        }
        return new TextBlock
        {
            Text = "Not Found: " + name,
        };
    }

    public bool Match(object? data)
    {
        return data is ObservableObject;
    }
}
