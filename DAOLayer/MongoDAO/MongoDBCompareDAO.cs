using DAOLayer.Entities;
using DAOLayer.Services;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace DAOLayer.DAO
{
    public class MongoDBCompareDAO : IDAO<CompareResult>
    {
        private string DBName;
        private string CollectionName;
        public MongoDBCompareDAO(string dbName, string collectionName)
        {
            DBName = dbName;
            CollectionName = collectionName;
        }

        public void Create(IConnection c, CompareResult t)
        {
            var collection = GetCollection(c);

            collection.InsertOne(t);
        }

        public List<CompareResult> Read(IConnection c, int start, int count)
        {
            var collection = GetCollection(c);
            var allData = collection.Find(new BsonDocument()).ToList<CompareResult>();
            if(start < allData.Count)
                return allData.GetRange(start, start + count > allData.Count ? allData.Count - start: count);

            return new List<CompareResult>();
        }

        public List<CompareResult> ReadAll(IConnection c)
        {
            var collection = GetCollection(c);
            return collection.Find(new BsonDocument()).ToList<CompareResult>();
        }

        public void Update(IConnection c, CompareResult t)
        {
            var collection = GetCollection(c);
            collection.ReplaceOne(t.GetEqualFilter(), t);
        }

        private IMongoCollection<CompareResult> GetCollection(IConnection mgConnection)
        {
            return ((MongoDBConnection)mgConnection)?.Client?.GetDatabase(DBName)?.GetCollection<CompareResult>(CollectionName);
        }
    }
}
