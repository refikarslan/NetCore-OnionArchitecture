using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore_OnionArchitecture.Application.Interfaces;
using Scrutor;
using System.Reflection;
using NetCore_OnionArchitecture.Domain.Common.DI;
using NetCore_OnionArchitecture.Shared.Services;

namespace NetCore_OnionArchitecture.Shared
{
    public static class ServiceExtension
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDateTimeService, DateTimeService>();


            services.Scan(scan => scan
             .FromAssemblyDependencies(Assembly.GetExecutingAssembly()) //Scan the assembly containing

             .AddClasses(classes => classes.AssignableTo<ITransientDependency>())  // Add classes that implement ITransientDependency
             .UsingRegistrationStrategy(RegistrationStrategy.Skip) // skip duplicates
             .AsSelf()
             .AsSelfWithInterfaces()
             .AsMatchingInterface()
             .AsImplementedInterfaces()
             .WithTransientLifetime() // Use transient lifetime for the services

             .AddClasses(classes => classes.AssignableTo<IScopedDependency>())
             .UsingRegistrationStrategy(RegistrationStrategy.Skip)
             .AsSelf()
             .AsSelfWithInterfaces()
             .AsMatchingInterface()
             .AsImplementedInterfaces()
             .WithScopedLifetime() // Use Scoped lifetime for the services

             .AddClasses(classes => classes.AssignableTo<ISingletonDependency>())
             .UsingRegistrationStrategy(RegistrationStrategy.Skip)
             .AsSelf()
             .AsSelfWithInterfaces()
             .AsMatchingInterface()
             .AsImplementedInterfaces()
             .WithSingletonLifetime() //Use Singleton lifetime for the services
             );
        }
    }
}



