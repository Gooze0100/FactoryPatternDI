namespace FactoryPattern.Factories;

//builder.Services.AddTransient<ISample1, Sample1>();
//builder.Services.AddSingleton<Func<ISample1>>(x => () => x.GetService<ISample1>()!);

// it is the same as above
// builder.Services.AddAbstractFactory<ISample1, Sample1>();

public static class AbstractFactoryExtension
{
    // it will be extensions method for IServiceCollection
    public static void AddAbstractFactory<TInterface, TImplementation>(this IServiceCollection services)
        // we usually give interface and then class for dependency injection in chevrons
        where TInterface : class
        where TImplementation : class, TInterface
    {
        services.AddTransient<TInterface, TImplementation>();
        services.AddSingleton<Func<TInterface>>(x => () => x.GetService<TInterface>()!);
        // here we adding new class to dependency injection and the class is AbstractFactory with TInterface
        // here we want to have IAbstractFactory of TInterface
        services.AddSingleton<IAbstractFactory<TInterface>, AbstractFactory<TInterface>>();
    }
}

// this <T> is the same as TInterface because it is generic
public class AbstractFactory<T> : IAbstractFactory<T>
{
    private readonly Func<T> _factory;

    public AbstractFactory(Func<T> factory)
    {
        _factory = factory;
    }

    public T Create()
    {
        // returns execution of actual object
        return _factory();
    }

}

public interface IAbstractFactory<T>
{
    T Create();
}