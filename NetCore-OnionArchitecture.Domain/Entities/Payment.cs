using NetCore_OnionArchitecture.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore_OnionArchitecture.Domain.Entities
{
    public class Payment : Entity
    {
        public DateTime PaymentDate { get; set; }
        public int Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
    }
}
