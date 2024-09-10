using NetCore_OnionArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore_OnionArchitecture.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        #region Commands
        CreateMap<CreateCustomerCommand, Customer>();
           
        #endregion
    }
}
