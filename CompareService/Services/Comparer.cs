using DAOLayer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompareService
{
    public class Comparer: ICompare
    {
        private static Comparer _instance;
        private static object _S_Lock = new object();
        public static Comparer Instance
        {
            get
            {
                lock (_S_Lock)
                {
                    if (_instance == null)
                        _instance = new Comparer();
                }
                return _instance;
            }
        }

        async Task CompareTask(string userName, DateTime startTime, CompareInput compareInput)
        {
            //Run Compare Task
            await Task.Delay(10000);
            //R...

            //Update DB
            var connection = ConnectionFactory.Instance.GetMongoDBConnection();
            var compareDAO = DAOFactory.Instance.GetCompareDAO("webct", "_compareResults");
            compareDAO.Update(connection, new DAOLayer.Entities.CompareResult
            {
                UserName = userName,
                ProjectName = compareInput.ProjectName,
                Status = 2,
                Time = startTime,
                Message = "Comparing...",
                FileName1 = compareInput.FileName1,
                FileName2 = compareInput.FileName2
            });
        }


        public List<CompareResult> GetCompareHistory(string userName, int start, int count)
        {
            var connection = ConnectionFactory.Instance.GetMongoDBConnection();
            var compareDAO = DAOFactory.Instance.GetCompareDAO("webct", "_compareResults");

            var result = compareDAO.ReadAll(connection).FindAll(x => string.Equals(x.UserName, userName, StringComparison.InvariantCultureIgnoreCase));
            result.Sort();
            if(start < result.Count)
            {
                return result.GetRange(start, result.Count > start + count ? count: result.Count-start).ConvertAll(x => new CompareResult()
                {
                    UserName = x.UserName,
                    ProjectName = x.ProjectName,
                    Time = x.Time,
                    Status = x.Status,
                    Message = x.Message,
                    //Result = x.Result,
                    FileName1 = x.FileName1,
                    FileName2 = x.FileName2
                });
            }

            return new List<CompareResult>();

        }


        public CompareResult Start(string userName, CompareInput compareInput)
        {
            var startTime = DateTime.Now;

            //Start Compare
            Task.Run(() =>
            {
                CompareTask(userName, startTime, compareInput);
            });
            

            //Insert DB
            var connection = ConnectionFactory.Instance.GetMongoDBConnection();
            var compareDAO = DAOFactory.Instance.GetCompareDAO("webct", "_compareResults");
            compareDAO.Create(connection, new DAOLayer.Entities.CompareResult
            {
                UserName = userName,
                ProjectName = compareInput.ProjectName,
                Status = 1,
                Time = startTime,
                Message = "Comparing...",
                FileName1 = compareInput.FileName1,
                FileName2 = compareInput.FileName2
            });


            //Return Result
            return new CompareResult()
            {
                ProjectName = compareInput.ProjectName,
                Status = 1,
                Message = "Start Comparing..."
            };
        }
    }
}
