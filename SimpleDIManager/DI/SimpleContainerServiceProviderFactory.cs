

namespace SimpleDIManager.DI;

public class SimpleContainerServiceProviderFactory : IServiceProviderFactory<ISimpleDIContainer>
{
    public ISimpleDIContainer CreateBuilder(IServiceCollection services)
    {
        var provider = new SimpleDIContainer();

        foreach (var service in services)
        {
            if (service.ImplementationType != null)
            {
                Lifetime lifetime = Lifetime.Transient;

                switch (service.Lifetime)
                {
                    case ServiceLifetime.Singleton:
                        lifetime = Lifetime.Singleton;
                        break;
                    case ServiceLifetime.Scoped:
                        lifetime = Lifetime.Scopped;
                        break;
                    case ServiceLifetime.Transient:
                        lifetime = Lifetime.Transient;
                        break;
                    default:
                        break;
                }

                provider.Register(service.ServiceType, service.ImplementationType, lifetime);

            }
        }


        return provider;
    }

    public IServiceProvider CreateServiceProvider(ISimpleDIContainer containerBuilder)
    {
        return new SimpleDIProvider(containerBuilder);
    }
}
