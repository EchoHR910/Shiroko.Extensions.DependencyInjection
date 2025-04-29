namespace Shiroko.Extensions.DependencyInjection.PropertyInjection
{
    /// <summary>
    /// Attribute to mark properties for dependency injection.
    /// </summary>
    /// <remarks>
    /// This attribute is used to indicate that a property should be injected with a service from the dependency injection container.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property)]
    public class InjectAttribute : Attribute
    {
    }
}