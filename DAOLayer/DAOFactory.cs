using DAOLayer.DAO;
using DAOLayer.Entities;

namespace DAOLayer
{
    public class DAOFactory
    {
        private static DAOFactory _instance;
        private static object _s_lock = new object();
        public static DAOFactory Instance
        {
            get
            {
                lock (_s_lock)
                {
                    _instance = new DAOFactory();
                }
                return _instance;
            }
        }

        public IDAO<User> GetUserDAO(string dbName, string collectionName)
        {
            return new MongoDBUserDAO(dbName, collectionName);
        }

        public IDAO<CompareResult> GetCompareDAO(string dbName, string collectionName)
        {
            return new MongoDBCompareDAO(dbName, collectionName);
        }
    }
}
