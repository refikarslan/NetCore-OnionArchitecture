
using NetCore_OnionArchitecture.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using NetCore_OnionArchitecture.Domain.Common.Repositories;
using NetCore_OnionArchitecture.Domain.Common.UnitOfWork;
using NetCore_OnionArchitecture.Persistence.Context;
using NetCore_OnionArchitecture.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace NetCore_OnionArchitecture.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly Dictionary<Type, object> _repositories = new(); //Bu sözlük (Dictionary) alanı, UnitOfWork içinde kullanılan tüm repository (depo) nesnelerini saklamak için kullanılır. Amaç, aynı tipte bir repository'nin birden fazla kez oluşturulmasını önlemek.
        private IDbContextTransaction _transaction;//Bu alan, veritabanı işlemleri sırasında kullanılan bir transaction (işlem) nesnesini saklar. 
        //Amaç, birden fazla veritabanı işlemini otomik bir işlem olarak yönetmek, yani işlemlerden biri başarısız olursa, tüm işlemleri geri almak (rollback) ve veritabanını tutarlı bir durumda tutmak.
        //Mesela, bir kullanıcı kaydı ve bu kullanıcıya bağlı bir profil kaydı eklediğinizi düşünelim.İki ekleme işlemi de aynı transaction içinde gerçekleştirilir.Eğer her iki işlem de başarılı olursa transaction commit edilir, ancak herhangi biri başarısız olursa tüm işlemler geri alınır.
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void BeginTransaction() //BeginTransaction metodu ile transaction başlatılır.
        {
            _transaction = _dbContext.Database.BeginTransaction();
        }


        //Bu metot, mevcut işlemi tamamlar. Eğer işlem varsa, Commit ile değişiklikler kalıcı hale getirilir. Ardından, veritabanındaki değişiklikleri kaydetmek için SaveChanges metodu çağrılır.
        public int Complete() 
        {
            _transaction?.Commit();
            return _dbContext.SaveChanges();
        }


        //Bu metot, asenkron bir şekilde işlemi tamamlar. Eğer commitTransaction parametresi true ise, işlem kalıcı hale getirilir. Ardından, asenkron olarak veritabanındaki değişiklikler kaydedilir.
        public async Task<int> CompleteAsync(bool commitTransaction = false)   
        {
            if(commitTransaction && _transaction != null)
                _transaction.Commit();
            return await _dbContext.SaveChangesAsync();
        }


        //Dispose metodu, IDisposable arayüzü ile birlikte gelir. Kaynakların serbest bırakılması için çağrılır. Dispose(true) metodu, gerçekte kaynakların serbest bırakılmasını sağlar.
        public void Dispose()  
        {
            Dispose(true);
            GC.SuppressFinalize(this);  
        }

        protected virtual void Dispose(bool disposing)    
        {
            if (disposing)
            {
                if (_transaction != null)
                {
                    try
                    {
                        _transaction.Rollback();
                    }
                    catch (InvalidOperationException ex)
                    {
                        // Handle the exception caused by rolling back a completed or disposed transaction.
                        // You can log the exception or perform any other necessary error handling.
                    }
                    finally
                    {
                        _transaction.Dispose();
                    }
                }

                _dbContext.Dispose();
            }
        }



        // GetRepository<TEntity>() metodu, Belirli bir TEntity türü için repository nesnesini almak üzere çağrılan bir yardımcı metottur. int türünde birincil anahtarı olan entity'ler için bir repository döndürür.
        public IRepository<TEntity, int> GetRepository<TEntity>() where TEntity : class, IEntity<int>
        {
            return GetRepository<TEntity, int>();
        }



        // GetRepository<TEntity, TPrimaryKey>() metodu, 
        public IRepository<TEntity, TPrimaryKey> GetRepository<TEntity, TPrimaryKey>() where TEntity : class, IEntity<TPrimaryKey>
        {
            if (_repositories.TryGetValue(typeof(TEntity), out object repository))
            {
                return (IRepository<TEntity, TPrimaryKey>)repository;
            }

            var newRepository = new EfCoreRepositoryBase<TEntity, TPrimaryKey>(_dbContext);
            _repositories[typeof(TEntity)] = newRepository;

            return newRepository;
        }
    }
}
