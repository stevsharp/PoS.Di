
namespace SimpleDIManager.DI;

/// <summary>
/// https://learn.microsoft.com/en-us/dotnet/api/system.iserviceprovider?view=net-8.0
/// </summary>
public class ISimpleDIProvider : IServiceProvider
{
    public object? GetService(Type serviceType)
    {
        throw new NotImplementedException();
    }
}
