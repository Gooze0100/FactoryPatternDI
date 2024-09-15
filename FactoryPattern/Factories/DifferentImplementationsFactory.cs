// this means file scope, that there will be just one namespace per file
using FactoryPattern.Samples;

namespace FactoryPattern.Factories;

public static class DifferentImplementationsFactoryExtension
{
    public static void AddVehicleFactory(this IServiceCollection services)
    {
        // for standard version we would get last value Van because asking same IVehicle
        // but for now we are asking IEnumerable so it check where class name is the same
        services.AddTransient<IVehicle, Car>();
        services.AddTransient<IVehicle, Truck>();
        services.AddTransient<IVehicle, Van>();
        services.AddSingleton<Func<IEnumerable<IVehicle>>>(x => () => x.GetService<IEnumerable<IVehicle>>()!);
        services.AddSingleton<IVehicleFactory, VehicleFactory>();
    }

}

public interface IVehicleFactory
{
    IVehicle Create(string name);
}

// create interfaces in different files
public class VehicleFactory : IVehicleFactory
{
    private readonly Func<IEnumerable<IVehicle>> _factory;

    public VehicleFactory(Func<IEnumerable<IVehicle>> factory)
    {
        _factory = factory;
    }

    public IVehicle Create(string name)
    {
        var set = _factory();
        IVehicle output = set.Where(x => x.VehicleType == name).First();

        return output;
    }

}