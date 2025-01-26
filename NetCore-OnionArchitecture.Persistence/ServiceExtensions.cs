using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore_OnionArchitecture.Persistence.Context;
using NetCore_OnionArchitecture.Domain.Common.UnitOfWork;
using NetCore_OnionArchitecture.Persistence.Repositories;
using NetCore_OnionArchitecture.Domain.Common.Repositories;

namespace NetCore_OnionArchitecture.Persistence
{
    public static class ServiceExtensions
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            #region Repositories

            services.AddTransient(typeof(IRepository<,>), typeof(EfCoreRepositoryBase<,>));
            services.AddTransient(typeof(IRepository<>), typeof(EfCoreRepositoryBase<>));
            #endregion

            services.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();

            services.AddTransient<IAuthRepository, AuthRepository>();
           
        }
    }
}
