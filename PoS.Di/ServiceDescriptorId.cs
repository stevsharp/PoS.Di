using System;
using System.Collections.Generic;

namespace PoS.Di
{
    public class ServiceDescriptorId
    {
        public static ServiceDescriptorId Create(Type serviceID)
        {
            return new ServiceDescriptorId()
            {
                ID = serviceID
            };
        }

        public override bool Equals(object obj)
        {
            return obj is ServiceDescriptorId id &&
                   EqualityComparer<Type>.Default.Equals(ID, id.ID);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID);
        }

        public Type ID { get; private set; }


    }
}
