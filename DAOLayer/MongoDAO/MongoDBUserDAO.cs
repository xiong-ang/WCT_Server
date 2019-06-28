using DAOLayer.Entities;
using DAOLayer.Services;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace DAOLayer.DAO
{
    public class MongoDBUserDAO : IDAO<User>
    {
        private string DBName;
        private string CollectionName;
        public MongoDBUserDAO(string dbName, string collectionName)
        {
            DBName = dbName;
            CollectionName = collectionName;
        }

        public void Create(IConnection c, User t)
        {
            var collection = GetCollection(c);

            collection.InsertOne(t);
        }

        public List<User> Read(IConnection c, int start, int count)
        {
            var collection = GetCollection(c);
            return collection.Find(new BsonDocument()).ToList<User>().GetRange(start, count);
        }

        public List<User> ReadAll(IConnection c)
        {
            var collection = GetCollection(c);
            return collection.Find(new BsonDocument()).ToList<User>();
        }

        public void Update(IConnection c, User t)
        {
            var collection = GetCollection(c);
            collection.ReplaceOne(t.GetEqualFilter(), t);
        }

        private IMongoCollection<User> GetCollection(IConnection mgConnection)
        {
            return ((MongoDBConnection)mgConnection)?.Client?.GetDatabase(DBName)?.GetCollection<User>(CollectionName);
        }
    }
}
