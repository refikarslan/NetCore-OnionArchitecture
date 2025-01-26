using NetCore_OnionArchitecture.Domain.Common.Entities;
using NetCore_OnionArchitecture.Domain.Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore_OnionArchitecture.Domain.Common.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity, TPrimaryKey> GetRepository<TEntity, TPrimaryKey>() where TEntity : class, IEntity<TPrimaryKey>;
        //IEntity<TPrimaryKey> : TEntity, IEntity<TPrimaryKey> arayüzünü uygulamalıdır. Bu, TEntity'nin birincil anahtar türü olan TPrimaryKey türüne sahip olmasını sağlar.
        //Bu metot, belirli bir TEntity ve TPrimaryKey türüne sahip bir repository (depo) nesnesi döndürür.

        IRepository<TEntity, int> GetRepository<TEntity>() where TEntity : class, IEntity<int>;//Bu metot, belirli bir TEntity türüne sahip ve int türünde birincil anahtara (primary key) sahip olan bir repository nesnesi döndürür.

        int Complete();//Bu metot, yapılan değişiklikleri veritabanına kaydeder ve etkilenen kayıtların sayısını döndürür.

        Task<int> CompleteAsync(bool commitTransaction = false);

        void BeginTransaction(); //Bu metot, yeni bir veritabanı işlemi (transaction) başlatır.
    }
}
