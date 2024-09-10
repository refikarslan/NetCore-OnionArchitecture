using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NetCore_OnionArchitecture.Domain.Common.Entities
{
    [Serializable]
    public abstract class Entity : Entity<int>, IEntity
    {

    }
    //Serializable kullanılan durumlar : 
    //Durum Saklama:Uygulamanın kapanıp açılmasından sonra nesnelerin durumunu korumak için.
    //Veri Aktarımı: Ağ üzerinden veya farklı süreçler arasında veri aktarımı yapmak için.
    //Bellek Yönetimi: Bellek boşaltma ve geri yükleme işlemleri için.
    //Dağıtık Sistemler: Dağıtık sistemlerde nesne transferi yapmak için.

    [Serializable]
    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        public virtual TPrimaryKey Id { get; set; }

        public virtual bool IsTransient() 
        {
            if(EqualityComparer<TPrimaryKey>.Default.Equals(Id, default(TPrimaryKey)))
            {
                return true;
            }

            if(typeof(TPrimaryKey) == typeof(int))
            {
                return Convert.ToInt32(Id) <= 0;
            }

            if(typeof(TPrimaryKey) == typeof(long))
            {
                return Convert.ToInt64(Id) <= 0;
            }
            return false;
        }

        public virtual bool EntityEquals(object obj)
        {
            if (obj == null || !(obj is Entity<TPrimaryKey>))
            {
                return false;
            }

            //Same instances must be considered as equal
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            //Transient objects are not considered as equal
            var other = (Entity<TPrimaryKey>)obj;
            if (IsTransient() && other.IsTransient())
            {
                return false;
            }

            //Must have a IS-A relation of types or must be same type
            var typeOfThis = GetType();
            var typeOfOther = other.GetType();
            if (!typeOfThis.GetTypeInfo().IsAssignableFrom(typeOfOther) && !typeOfOther.GetTypeInfo().IsAssignableFrom(typeOfThis))
            {
                return false;
            }

            return Id.Equals(other.Id);
        }

        public override string ToString()
        {
            return $"[{GetType().Name} {Id}]";
        }
    }
}
