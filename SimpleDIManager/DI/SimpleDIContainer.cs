

namespace SimpleDIManager.DI
{
    public class SimpleDIContainer : ISimpleDIContainer
    {
        private readonly Dictionary<Type, (Type service, Lifetime lifetime , int threadId)> _registrations = new();

        private readonly Dictionary<Type, object> _singletonCollection = new();

        public void Register<TService, TImplementation>(Lifetime lifetime = Lifetime.Transient) where TImplementation : TService
        {
            _registrations[typeof(TService)] = (typeof(TImplementation), lifetime, Thread.CurrentThread.ManagedThreadId);
        }

        public TService Resolve<TService>()
        {
            return (TService)ResolveType(typeof(TService));
        }

        private object ResolveType(Type service)
        {
            ArgumentNullException.ThrowIfNull(service);

            var threadId = Thread.CurrentThread.ManagedThreadId;

            if (_registrations.ContainsKey(service))
            {

                var (implementationType, lifetime, thredId) = _registrations[service];

                switch (lifetime)
                {
                    case Lifetime.Singleton:
                        
                        if (!_singletonCollection.ContainsKey(service))
                        {
                            _singletonCollection[service] = CreateType(implementationType);
                        }
                        return _singletonCollection[service];

                    case Lifetime.Transient:

                        return CreateType(implementationType);  

                    case Lifetime.Scopped:

                        break;
                }
            }


            throw new InvalidOperationException($"Service of type {service.Name} is not registered.");
        }

        private object CreateType(Type service)
        {
            var constructor = service.GetConstructors().First();

            var paramters = constructor.GetParameters()
                                    .Select(x => ResolveType(x.ParameterType))
                                    .ToArray();

            return Activator.CreateInstance(service, paramters);

        }
    }
}