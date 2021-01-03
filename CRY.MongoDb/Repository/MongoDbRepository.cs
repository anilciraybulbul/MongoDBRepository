using CRY.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System;
using System.Linq;

namespace CRY.MongoDb.Repository
{
    public class MongoDbRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        private readonly IMongoDatabaseProvider _databaseProvider;

        public MongoDbRepository(
            IMongoDatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
        }

        public MongoDatabase Database
        {
            get { return _databaseProvider.Database; }
        }

        public  MongoCollection<TEntity> Collection
        {
            get
            {
                return _databaseProvider.Database.GetCollection<TEntity>(typeof(TEntity).Name);
            }
        }

        public IQueryable<TEntity> GetAll()
        {
            return Collection.AsQueryable();
        }

        public TEntity Get(TPrimaryKey id)
        {
            var query = Query<TEntity>.EQ(e => e.Id, id);
            var entity = Collection.FindOne(query);
            if (entity == null)
            {
                throw new Exception("There is no such an entity with given primary key");
            }
            return entity;
        }

        public TEntity FirstOrDefault(TPrimaryKey id)
        {
            var query = Query<TEntity>.EQ(e => e.Id, id);
            return Collection.FindOne(query);
        }

        public TEntity Insert(TEntity entity)
        {
            Collection.Insert(entity);
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            Collection.Save(entity);
            return entity;
        }

        public void Delete(TEntity entity)
        {
            Delete(entity.Id);
        }

        public void Delete(TPrimaryKey id)
        {
            var query = Query<TEntity>.EQ(e => e.Id, id);
            Collection.Remove(query);
        }
    }
}
