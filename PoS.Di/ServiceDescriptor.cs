using System;
using System.Collections.Generic;

namespace PoS.Di;

public class ServiceDescriptor
{
    public ServiceDescriptorId Service { get;  }
    public object Implementation { get;  }
    public Lifetime Lifetime { get; }

    public bool IsSingleton => Lifetime == Lifetime.Singleton;

    public bool IsScopped => Lifetime == Lifetime.Scopped;

    public bool IsTransient => Lifetime == Lifetime.Transient;

    public ServiceDescriptor(ServiceDescriptorId Service, object Implementation, Lifetime Lifetime)
    {
        this.Service = Service;
        this.Implementation = Implementation;   
        this.Lifetime = Lifetime;
    }

    public override bool Equals(object obj)
    {
        return obj is ServiceDescriptor descriptor &&
               EqualityComparer<Type>.Default.Equals(Service.ID, descriptor.Service.ID);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Service);
    }
}
