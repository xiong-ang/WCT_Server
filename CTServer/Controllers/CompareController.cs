using AuthService;
using CompareService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
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
        private string filePath = "~/App_Data/";


        [HttpPost]
        [Route("")]
        public CompareResult CreateCompare()
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

            string config = HttpContext.Current.Request["Config"];
            CompareInput compareInput = JsonConvert.DeserializeObject<CompareInput>(config);

            HttpFileCollection files = HttpContext.Current.Request.Files;
            if(files.AllKeys.Length != 2)
            {
                return new CompareResult()
                {
                    Status = 3,
                    Message = "Error Input"
                };
            }

            compareInput.FileName1 = SaveFile(files[0]);
            compareInput.FileName2 = SaveFile(files[1]);

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

        private string SaveFile(HttpPostedFile file)
        {
            if (file == null || string.IsNullOrWhiteSpace(file.FileName))
                return string.Empty;

            //Convert FileName
            string newFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

            file.SaveAs(HttpContext.Current.Server.MapPath(this.filePath) + newFileName);

            return newFileName;
        }
    }
}
