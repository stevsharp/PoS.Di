using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace PoS.Di
{
    public class Container : IContainer
    {
        private Dictionary<ServiceDescriptorId, ServiceDescriptor> _services = new();
        private readonly Dictionary<ServiceDescriptorId, object> _singletonCollection = new();

        private void Register(Type serviceType, Type implementationType, Lifetime lifetime = Lifetime.Transient)
        {
            var id = ServiceDescriptorId.Create(serviceType);

            var registration = new ServiceDescriptor(id, implementationType, lifetime);
            
            _services[id] = registration;
        }

        private object CreateType(Type service)
        {
            return Activator.CreateInstance(service);
        }

        public void AddTransient<TService, TImplementation>() where TImplementation : class, TService
        {
            Register(typeof(TService), typeof(TImplementation), Lifetime.Transient);
        }

        public void AddScopped<TService, TImplementation>() where TImplementation : class, TService
        {
            Register(typeof(TService), typeof(TImplementation), Lifetime.Scopped);
        }

        public void AddSingelton<TService, TImplementation>() where TImplementation : class, TService
        {
            Register(typeof(TService), typeof(TImplementation), Lifetime.Singleton);
        }


        public TService GetService<TService>() where TService : class
        {
            Type serviceType = typeof(TService);

            var registrationID = ServiceDescriptorId.Create(serviceType);

            var registration = _services[registrationID];

            if (registration == null)
                return null;

            if (registration.IsSingleton)
            {
                if (!_singletonCollection.ContainsKey(registrationID))
                    _singletonCollection[registrationID] = CreateType(registration.Implementation);

                return _singletonCollection[registrationID] as TService;
            }

            return CreateType(registration.Implementation) as TService;
        }

        public void Dispose()
        {
            //TODO(R): Near future
        }
    }
}
