using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace DAOLayer.Entities
{
    [BsonIgnoreExtraElements]
    public class User: MongoEntity<User>
    {
        [BsonElement("name")]
        public string UserName;
        [BsonElement("password")]
        public string Password;
        [BsonElement("email")]
        public string Email;

        public override FilterDefinition<User> GetEqualFilter()
        {
            return Builders<User>.Filter.Eq("name", this.UserName);
        }
    }
}
