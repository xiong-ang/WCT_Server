using AuthService;
using CompareService;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CTServer.Controllers
{
    /// <summary>
    /// Controller for file comparasion
    /// </summary>
    [AuthFilterAttribute]
    [RoutePrefix("api/compare")]
    public class CompareController : ApiController
    {
        private ICompare CompareSvc = Comparer.Instance;

        [HttpPost]
        [Route("")]
        public CompareResult CreateCompare([FromBody]CompareInput compareInput)
        {
            //Get User from header token
            string userName;
            var token = Request.Headers.FirstOrDefault(x => string.Equals(x.Key, "Authorization")).Value.ElementAt(0);
            JWToken.Validate(token, out userName);
            if (string.IsNullOrWhiteSpace(userName))
            {
                return new CompareResult()
                {
                    Status = 3,
                    Message = "Authorization failed."
                }; 
            }


            return CompareSvc.Start(userName, compareInput);
        }


        [HttpGet]
        [Route("history/{start}/{count}")]
        public List<CompareResult> GetCompareHistroy(int start, int count)
        {
            //Get User from header token
            string userName;
            var token = Request.Headers.FirstOrDefault(x => string.Equals(x.Key, "Authorization")).Value.ElementAt(0);
            JWToken.Validate(token, out userName);
            if (string.IsNullOrWhiteSpace(userName))
            {
                return new List<CompareResult>();
            }


            return CompareSvc.GetCompareHistory(userName, start, count);
        }
    }
}
