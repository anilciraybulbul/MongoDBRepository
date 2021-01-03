using MongoDB.Driver;

namespace CRY.MongoDb
{
    public interface IMongoDatabaseProvider
    {
        MongoDatabase Database { get; }
    }
}
