# Shiroko.Extensions.DependencyInjection

## Introduction

This is a .NET-based dependency injection extension library that supports automatic dependency injection via the `[Inject]` attribute.  
It extends `Microsoft.Extensions.DependencyInjection`, making it easier for developers to manage service dependencies.

## Features
- **Property Injection**: Automatically injects dependencies into service properties marked with `[Inject]`.

## Quick Start

### 1. Register Service
**Enable Property Injection**: Call `UsePropertyInjection()` to extend `IServiceCollection`.
```csharp
using Shiroko.Extensions.DependencyInjection.PropertyInjection;
...
service.UsePropertyInjection(); //Use IServiceCollection Extension to Register Service;

```
### 2. Define Services
```csharp
using Shiroko.Extensions.DependencyInjection.PropertyInjection;

public class ExampleService
{
    [Inject] DependencyService Dependency { get; set; }
}

```
### 3. Resolve Service
**Resolve Services**: Use `IResolvedService<T>` to get the service instance with injected properties.
```csharp
using Shiroko.Extensions.DependencyInjection.PropertyInjection;

public class SomeViewModel
{
    private readonly ExampleService _exampleService;

    public SomeViewModel(IResolvedService<ExampleService> exampleService)
    {
       _exampleService = exampleService.Service;
    }
}
```
Or
```csharp
using Shiroko.Extensions.DependencyInjection.PropertyInjection;

IResolvedService resolver = serviceProvider.GetRequiredService<IResolvedService<ExampleService>();
ExampleService service = resolver.Service;

```

