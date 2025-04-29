namespace Shiroko.Extensions.DependencyInjection.PropertyInjection
{
    public class ServiceResolutionException : Exception
    {
        public ServiceResolutionException()
        { }

        public ServiceResolutionException(string message) : base(message)
        {
        }

        public ServiceResolutionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}