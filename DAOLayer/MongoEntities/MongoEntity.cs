using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace DAOLayer.Entities
{
    public interface IMongoEntity
    {
        ObjectId Id { get; set; }
    }

    public abstract class MongoEntity<T> : IMongoEntity
    {
        [BsonIgnoreIfDefault]
        [BsonId]
        public ObjectId Id { get; set; }

        public abstract FilterDefinition<T> GetEqualFilter();
    }
}
