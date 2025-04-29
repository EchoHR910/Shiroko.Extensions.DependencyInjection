using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shiroko.Extensions.DependencyInjection.PropertyInjection;

var container = new ServiceCollection();

container.UsePropertyInjection();
container.TryAddTransient(_ => new DependencyService("Hello, World!"));
container.TryAddTransient<TestService>();

var services = container.BuildServiceProvider();

var requiredService = services.GetRequiredService<IResolvedService<TestService>>();

requiredService.Service.Dependency.Say();


internal class TestService
{

    public TestService(DependencyService service2)
    {
        service2.Say("Hello");
    }

    [Inject]
    public DependencyService Dependency { get; set; } = null!;
}


internal class DependencyService(string words)
{
    public void Say()
    {
        Console.WriteLine($"Dependency Service Says: {words}");
    }

    public void Say(string words)
    {
        Console.WriteLine($"Dependency Service From Constructor Says: {words}");
    }
}
