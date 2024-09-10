using NetCore_OnionArchitecture.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore_OnionArchitecture.Domain.Entities
{
    public class Customer : AuditableBaseEntity
    {
        private int _age;

        public string Name { get; set; } 
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Tel { get; set; }
        public string EMail { get; set; }
        public string Address {  get; set; }

        public int Age
        {
            get
            {
                if(this._age >= 0)
                {
                    this._age = new DateTime(DateTime.Now.Subtract(this.DateOfBirth).Ticks).Year - 1;
                }
                return this._age;
            }
        }
          
    }
}
