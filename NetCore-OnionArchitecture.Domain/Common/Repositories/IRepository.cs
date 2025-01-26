using NetCore_OnionArchitecture.Domain.Common.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace NetCore_OnionArchitecture.Domain.Common.Repositories
{
    public interface IRepository : ITransientDependency
    {
    }
}
