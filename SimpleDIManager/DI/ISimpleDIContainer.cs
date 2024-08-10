


namespace SimpleDIManager.DI;

    public interface ISimpleDIContainer
    {
        void Register<TService, TImplementation>(Lifetime lifetime = Lifetime.Transient) where TImplementation : TService;
        TService Resolve<TService>();
    }
