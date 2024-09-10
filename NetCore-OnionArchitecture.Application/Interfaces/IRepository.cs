using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore_OnionArchitecture.Application.Interfaces
{
    public interface IRepository<T> : IRepositoryBase<T> where T : class
    {

    }
    public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class 
    {

    }
}
