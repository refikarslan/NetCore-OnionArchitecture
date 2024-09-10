using NetCore_OnionArchitecture.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore_OnionArchitecture.Domain.Entities
{
    public class Order : Entity
    {
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }
        public int PaymentId { get; set; }
        public User User { get; set; }
        public Payment Payment { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
