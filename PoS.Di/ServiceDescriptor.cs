using System;
using System.Collections.Generic;

namespace PoS.Di
{
    public class ServiceDescriptor
    {
        public ServiceDescriptorId Service { get; set; }
        public Type Implementation { get; set; }
        public Lifetime Lifetime { get; set; }

        public bool IsSingleton => Lifetime == Lifetime.Singleton;

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
}
