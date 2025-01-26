using NetCore_OnionArchitecture.Domain.Common;
using NetCore_OnionArchitecture.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore_OnionArchitecture.Domain.Entities
{
    public class Customer : Entity
    {
       
        public string Name { get; set; } 
        public string Surname { get; set; }
        public int Age { get; set; } 
        public DateTime DateOfBirth { get; set; }
        public string Tel { get; set; }
        public string EMail { get; set; }
        public string Address {  get; set; }

    }
}
