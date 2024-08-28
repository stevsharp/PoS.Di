using System;

namespace PoS.Di;

public interface IContainer : IDisposable
{
    void AddTransient<TService, TImplementation>() where TImplementation : class, TService;
    void AddScopped<TService, TImplementation>() where TImplementation : class, TService;
    void AddSingelton<TService, TImplementation>() where TImplementation : class, TService;
    TService GetService<TService>() where TService : class;
}