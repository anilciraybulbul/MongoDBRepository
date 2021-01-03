using Bogus;
using CRY.MongoDb;
using CRY.MongoDb.Configuration;
using CRY.MongoDb.Repository;
using CRY.Tests.Entities;
using NUnit.Framework;

namespace CRY.Tests
{
    public class MongoDbRepository
    {
        private readonly IMongoDatabaseProvider _mongoDatabaseProvider;
        private readonly IRepository<TestProduct, int> _repository;
        private readonly Faker<TestProduct> _testProduct;

        [System.Obsolete]
        public MongoDbRepository()
        {
            _mongoDatabaseProvider = new MongoDatabaseProvider(
                new MongoDbConfiguration
                {
                    ConnectionString = "",
                    DatabaseName = "",
                });

            _repository = new MongoDbRepository<TestProduct, int>(_mongoDatabaseProvider);
            _testProduct = new Faker<TestProduct>()
                   .CustomInstantiator(f =>
                   {
                       return new TestProduct
                       {
                           Id = f.Database.Random.Int(0, 1000),
                           Name = f.Database.Random.String(10)
                       };
                   });
        }

        [SetUp]
        public void Setup()
        {
            _mongoDatabaseProvider.Database.DropCollection("TestProduct");
        }

        [Test , Order(1)]
        public void Insert()
        {
            TestProduct entity = _repository.Insert(_testProduct.Generate());

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.Id);
        }

        [Test, Order(2)]
        public void Get()
        {
            TestProduct entityInsert = _repository.Insert(_testProduct.Generate());
            TestProduct entity = _repository.Get(entityInsert.Id);

            Assert.IsNotNull(entity);
            Assert.AreEqual(entityInsert.Id, entity.Id);
        }

        [Test, Order(3)]
        public void Update()
        {
            var entity = new TestProduct
            {
                Id = 1,
                Name = "Test Product"
            };

            entity = _repository.Insert(entity);

            string oldname = entity.Name;
            string newname = "Product Test";
            entity.Name = newname;

            entity = _repository.Update(entity);

            Assert.AreEqual(newname, entity.Name);
            Assert.AreNotEqual(oldname, entity.Name);
        }
    }
}