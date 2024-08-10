
namespace SimpleDIManager.DI
{
    public class SimpleContainerServiceProviderFactory : IServiceProviderFactory<SimpleDIProvider>
    {
        public SimpleDIProvider CreateBuilder(IServiceCollection services)
        {
            throw new NotImplementedException();
        }

        public IServiceProvider CreateServiceProvider(SimpleDIProvider containerBuilder)
        {
            throw new NotImplementedException();
        }
    }
}
