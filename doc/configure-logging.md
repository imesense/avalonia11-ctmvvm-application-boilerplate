# Configure Logging

Logging is configured via `appsettings.json` and `appsettings.Development.json` files using __Microsoft.Extensions.Logging__.

Default log levels:

- __Default__: `Information`
- __Microsoft__: `Warning`
- __System__: `Warning`

In development mode:

- __Default__: `Debug`
- __Microsoft__: `Information`
- __System__: `Information`

To inject logger into a class:

```csharp
public class MyService
{
    private readonly ILogger _logger;

    public MyService(ILogger<MyService> logger)
    {
        _logger = logger;
    }

    public void DoWork()
    {
        _logger.LogInformation("Doing work...");
    }
}
```
