using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore_OnionArchitecture.Domain.Common.Entities
{
  
    // Temel entityler için interface tanımlar. Sistemdeki tüm entityler bu interface'i uygulaması gerekir.
    public interface IEntity<TPrimaryKey>
    {
        //Entity'ler için benzersiz bir Id tanımladık.
        TPrimaryKey Id { get; set; }

        // bool IsTransient() yöntemi, bir varlığın (entity) kalıcı bir depolama sistemine (örneğin, bir veritabanı) henüz kaydedilip kaydedilmediğini kontrol etmek için kullanılır. Bu yöntemin amacı, varlığın kalıcı bir kimliğe (ID) sahip olup olmadığını belirlemektir. Genellikle, bir varlığın kimliği (ID'si) veritabanına kaydedildikten sonra oluşturulur ve bu ID, varlığın benzersiz tanımlayıcısı olarak kullanılır.
        bool IsTransient();
    }
} 
