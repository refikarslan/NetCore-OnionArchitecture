using NetCore_OnionArchitecture.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore_OnionArchitecture.Domain.Entities
{
    public class Address : Entity
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostaCode { get; set; }
        public string Country { get; set; }
        public bool IsDefault { get; set; }
        public string Detail { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
