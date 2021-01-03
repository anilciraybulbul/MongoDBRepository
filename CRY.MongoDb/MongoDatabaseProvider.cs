using CRY.MongoDb.Configuration;
using MongoDB.Driver;
using System;

namespace CRY.MongoDb
{
    public class MongoDatabaseProvider : IMongoDatabaseProvider
    {
        public MongoDatabase Database { get; private set; }

        [Obsolete]
        public MongoDatabaseProvider(
            IMongoDbConfiguration configuration)
        {
            Database = new MongoClient(configuration.ConnectionString)
               .GetServer()
               .GetDatabase(configuration.DatabaseName);
        }
    }
}
