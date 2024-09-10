using NetCore_OnionArchitecture.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetCore_OnionArchitecture.Domain.Entities
{
    public class User : Entity
    {
        public string UserName { get; set; }
        public string EMail { get; set; }
        public string PasswordHash { get; set; }
        public string UserFullName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Role { get; set; }

        public ICollection<Cart> Carts { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
}
