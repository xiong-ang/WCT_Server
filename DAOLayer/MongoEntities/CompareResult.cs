using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;

namespace DAOLayer.Entities
{
    [BsonIgnoreExtraElements]
    public class CompareResult: MongoEntity<CompareResult>, IComparable<CompareResult>
    {
        [BsonElement("username")]
        public string UserName;

        [BsonElement("projectname")]
        public string ProjectName;

        [BsonElement("time")]
        public DateTime Time;

        [BsonElement("status")]
        public int Status = 0; //0-init;1-runing;2-succeed;3-failed

        [BsonElement("message")]
        public string Message;

        [BsonElement("file1")]
        public string FileName1;

        [BsonElement("file2")]
        public string FileName2;

        [BsonElement("result")]
        public CResult Result;

        [BsonIgnoreExtraElements]
        public class CResult
        {
        }

        public override FilterDefinition<CompareResult> GetEqualFilter()
        {
            var filter1 = Builders<CompareResult>.Filter.Eq("username", this.UserName);
            var filter2 = Builders<CompareResult>.Filter.Eq("projectname", this.ProjectName);
            var filter3 = Builders<CompareResult>.Filter.Eq("time", this.Time);
            FilterDefinition<CompareResult>[] filters = { filter1, filter2, filter3 };

            return Builders<CompareResult>.Filter.And(filters);
        }


        public int CompareTo(CompareResult other)
        {
            return other.Time.Ticks - this.Time.Ticks> 0 ? 1:-1;
        }
    }
}
