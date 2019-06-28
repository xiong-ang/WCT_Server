using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOLayer.Services
{
    public class MongoDBConnection : IConnection
    {
        private string ConnectionString = "mongodb://localhost:27017";

        private MongoClient _client;
        private object _lock = new object();
        public MongoClient Client
        {
            get
            {
                Connect();
                return _client;
            }
        }

        public void Connect()
        {
            lock (_lock)
            {
                if (null == _client)
                    _client = new MongoClient(ConnectionString);
            }
        }
        public void Close()
        {
        }
    }
}
