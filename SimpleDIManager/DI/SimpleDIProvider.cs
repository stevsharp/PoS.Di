

namespace SimpleDIManager.DI;

public class SimpleDIProvider : IServiceProvider
{

    private readonly ISimpleDIContainer _serviceProvider; 

    public SimpleDIProvider(ISimpleDIContainer simpleDIContainer)
    {
        _serviceProvider = simpleDIContainer;
    }

    public object? GetService(Type serviceType)
    {
        return _serviceProvider.ResolveType(serviceType);
    }
}