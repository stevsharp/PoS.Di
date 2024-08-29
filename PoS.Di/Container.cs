using PoS.Di.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PoS.Di;

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

    private object CreateType(object service)
    {
        return CreateType((Type)service);
    }

    private object CreateType(Type service)
    {

        Object instance = null;
        Object instanceParam = null;

        try
        {

            var registrationID = ServiceDescriptorId.Create(service);

            if (_services.ContainsKey(registrationID))
            {
                instanceParam = _services[registrationID].Implementation;

                return instanceParam;
            }

            var constructor = service.GetConstructor();
            if (constructor is null)
                throw new InvalidOperationException($"No suitable constructor found for {service.FullName}.");

            if (constructor is null)
                return Activator.CreateInstance(service);

            var arguments = constructor.GetConstructorArguments();

            if (arguments.Any())
            {
                List<object> trace = new(arguments.Count());

                foreach (var argument in arguments)
                {

                    Type type = (Type)argument;

                    if (_services.ContainsKey(ServiceDescriptorId.Create(type)))
                    {
                        instanceParam = _services[ServiceDescriptorId.Create(type)].Implementation;

                        trace.Add(Activator.CreateInstance((Type)instanceParam));
                    }
                }

                instance = Activator.CreateInstance(service, trace.ToArray());

                return instance;
            }

            instance = Activator.CreateInstance(service);


            return instance;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to create instance of type {service.FullName}.", ex);
        }

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

    private object TryGetInstanceIfSingleton(Type type)
    {
        var registrationID = ServiceDescriptorId.Create(type);

        return this._singletonCollection.TryGetValue(registrationID, out var singleton) ? singleton : null;
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

        if (registration.IsTransient)
        {
            if (_services.ContainsKey(registrationID))
            {
                var implementation = CreateType(registration.Implementation);

                var serviceDescriptor = new ServiceDescriptor(ServiceDescriptorId.Create(serviceType), implementation, Lifetime.Transient);

                _services[registrationID] = serviceDescriptor;

                return _services[registrationID].Implementation as TService;
            }

            throw new InvalidOperationException($"Not Valid Implementation {registration.Implementation}");

        }

        if (registration.IsScopped)
        {

        }

        return CreateType(registration.Implementation) as TService;
    }

    public void Dispose()
    {
        //TODO(R): Near future
    }
}


//if (parameterInstances.Any())
//{
//    try
//    {
//        var types = new List<Type>();

//        foreach (var parameterInstance in parameterInstances)
//        {
//            //var oo = ServiceDescriptorId.Create(parameterInstance);

//            //var ooo = _services[oo];


//            //types.Add(typeof(parameterInstance));
//        }



//        return Activator.CreateInstance(service, parameterInstances.First());
//    }
//    catch (Exception ex)
//    {
//        var message = ex.Message;   
//    }

//}


//var constructor = service.GetConstructors()
//                             .OrderByDescending(c => c.GetParameters().Length)
//                             .FirstOrDefault();

//var parameters = constructor.GetParameters();

//var parameterInstances = parameters.Select(p => p.ParameterType).ToArray();

//private static ConstructorInfo GetConstructor(Type type) =>
//       type.GetConstructors()
//           .OrderByDescending(c => c.GetParameters().Length)
//           .FirstOrDefault();

//private object[] GetConstructorArguments(ConstructorInfo constructor)
//{

//    return constructor.GetParameters()
//        .Select(param => param.ParameterType)
//        .ToArray();

//    //return constructor.GetParameters()
//    //    .Select(param => CreateType(param.ParameterType))
//    //    .ToArray();
//}