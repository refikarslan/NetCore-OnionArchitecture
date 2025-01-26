using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore_OnionArchitecture.Application.DTOs.Common
{
    public class NameValueDto<T>
    {
        public string Name { get; set; }
        public T Value { get; set; }
    }
}
