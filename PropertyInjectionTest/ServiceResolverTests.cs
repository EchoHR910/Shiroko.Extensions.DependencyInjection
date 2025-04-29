using Moq;
using Shiroko.Extensions.DependencyInjection.PropertyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace PropertyInjectionTest
{
    [TestClass]
    public class ServiceResolverTests
    {
        [AllowNull]
        private Mock<IServiceProvider> _serviceProviderMock;

        [TestMethod]
        public void Constructor_ShouldResolveServiceAndInjectProperties()
        {
            // Arrange
            var mockService = new Mock<TestService>();

            var testService = new TestService();
            var dependencyService = new DependencyService();

            _serviceProviderMock.Setup(sp => sp.GetService(typeof(TestService))).Returns(testService);
            _serviceProviderMock.Setup(sp => sp.GetService(typeof(DependencyService))).Returns(dependencyService);

            // Act
            var resolver = new ServiceResolver<TestService>(_serviceProviderMock.Object);

            // Assert
            Assert.AreEqual(testService, resolver.Service);
        }

        [TestMethod]
        public void InjectProperties_ShouldInjectDependencies()
        {
            // Arrange
            var testService = new TestService();
            var dependency = new DependencyService();
            _serviceProviderMock.Setup(sp => sp.GetService(typeof(DependencyService))).Returns(dependency);

            // Act
            var injectPropertiesMethod = typeof(ServiceResolver<TestService>)
                .GetMethod("InjectProperties", BindingFlags.NonPublic | BindingFlags.Static);
            injectPropertiesMethod!.Invoke(null, [_serviceProviderMock.Object, testService]);

            // Assert
            Assert.AreEqual(dependency, testService.Dependency);
        }

        [TestMethod]
        public void InjectProperties_ShouldThrowIfDependencyNotResolved()
        {
            // Arrange
            var testService = new TestService();
            _serviceProviderMock.Setup(sp => sp.GetService(typeof(DependencyService))).Returns(null!);

            // Act
            var injectPropertiesMethod = typeof(ServiceResolver<TestService>)
                .GetMethod("InjectProperties", BindingFlags.NonPublic | BindingFlags.Static);

            var exception = Assert.ThrowsException<TargetInvocationException>(() => injectPropertiesMethod!.Invoke(null, [_serviceProviderMock.Object, testService])); ;

            Assert.IsInstanceOfType(exception.InnerException, typeof(ServiceResolutionException));
        }

        [TestMethod]
        public void ResolveService_ShouldThrowIfServiceNotResolved()
        {
            // Arrange
            _serviceProviderMock.Setup(sp => sp.GetService(typeof(TestService))).Returns(null!);

            // Act
            Assert.ThrowsException<ServiceResolutionException>(() => new ServiceResolver<TestService>(_serviceProviderMock.Object));
        }

        [TestInitialize]
        public void Setup()
        {
            _serviceProviderMock = new Mock<IServiceProvider>();
        }

        public class DependencyService
        { }

        // 测试用的服务类
        public class TestService
        {
            [AllowNull, Inject]
            public DependencyService Dependency { get; set; }
        }
    }
}