using NetCore_OnionArchitecture.Domain.Common.Entities;
using NetCore_OnionArchitecture.Domain.Common.Repositories;
using NetCore_OnionArchitecture.Persistence.Context;


namespace NetCore_OnionArchitecture.Persistence.Repositories
{
    public class EfCoreRepositoryBase<TEntity> : EfCoreRepositoryBase<TEntity, int>, IRepository<TEntity>
          where TEntity : class, IEntity<int>
    {
        public EfCoreRepositoryBase(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
