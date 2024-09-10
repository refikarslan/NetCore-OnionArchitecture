using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore_OnionArchitecture.Domain.Common
{
    public abstract class AuditableBaseEntity
    {
        //AuditableBaseEntity, nesne üzerinde yapılan işlemleri(örneğin, kim tarafından ve ne zaman güncellendi) izlemek için kullanılır.
        public virtual int Id {  get; set; }
        public  string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; } 
    }
}
  