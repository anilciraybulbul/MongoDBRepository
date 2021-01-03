using CRY.Entities;
using System.Linq;

namespace CRY.MongoDb.Repository
{
    public interface IRepository<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        IQueryable<TEntity> GetAll();

        TEntity Get(TPrimaryKey id);

        TEntity FirstOrDefault(TPrimaryKey id);

        TEntity Insert(TEntity entity);

        TEntity Update(TEntity entity);

        void Delete(TEntity entity);

        void Delete(TPrimaryKey id);
    }
}
