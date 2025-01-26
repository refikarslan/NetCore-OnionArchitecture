using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore_OnionArchitecture.Application.DTOs
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string EMail { get; set; }
        public string PasswordHash { get; set; }
        public string UserFullName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Role { get; set; }

    }
}
