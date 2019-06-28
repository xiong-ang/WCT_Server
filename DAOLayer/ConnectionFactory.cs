using DAOLayer.Services;

namespace DAOLayer
{
    public class ConnectionFactory
    {
        private static ConnectionFactory _instance;
        private static object _s_lock = new object();
        public static ConnectionFactory Instance
        {
            get
            {
                lock (_s_lock)
                {
                    _instance = new ConnectionFactory();
                }
                return _instance;
            }
        }

        public IConnection GetMongoDBConnection()
        {
            return new MongoDBConnection();
        }

    }
}
