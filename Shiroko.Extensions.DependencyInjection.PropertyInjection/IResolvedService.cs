namespace Shiroko.Extensions.DependencyInjection.PropertyInjection
{
    /// <summary>
    /// Represents a service that has been resolved and properties are injected.
    /// </summary>
    /// <typeparam name="TService">Specifies the type of service that can be retrieved, constrained to reference types.</typeparam>
    public interface IResolvedService<TService> where TService : class
    {
        TService Service { get; }
    }
}