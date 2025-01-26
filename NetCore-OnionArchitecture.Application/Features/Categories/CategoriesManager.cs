using NetCore_OnionArchitecture.Domain.Common.DI;
using NetCore_OnionArchitecture.Domain.Common.Repositories;
using NetCore_OnionArchitecture.Domain.Common.UnitOfWork;
using NetCore_OnionArchitecture.Domain.Entities;

namespace NetCore_OnionArchitecture.Application.Features.Categories 
{
    public class CategoriesManager : ITransientDependency
    {
        private IUnitOfWork unitOfWork;
        private IRepository<Category, int> _categoryRepository;
        
        
    }
}
 