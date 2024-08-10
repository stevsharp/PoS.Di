
namespace SimpleDIManager.DI;

public class SimpleDIProvider : ISimpleDIProvider
{

    private readonly ISimpleDIContainer _serviceProvider; 

    public SimpleDIProvider(ISimpleDIContainer simpleDIContainer)
    {
        _serviceProvider = simpleDIContainer;
    }

}