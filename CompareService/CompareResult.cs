using Newtonsoft.Json;
using System;

namespace CompareService
{
    [JsonObject]
    public class CompareResult
    {
        [JsonProperty("username")]
        public string UserName;

        [JsonProperty("projectname")]
        public string ProjectName;

        [JsonProperty("time")]
        public DateTime Time;

        [JsonProperty("status")]
        public int Status = 0; //0-init;1-runing;2-succeed;3-failed

        [JsonProperty("message")]
        public string Message;

        [JsonProperty("file1")]
        public string FileName1;

        [JsonProperty("file2")]
        public string FileName2;

        [JsonProperty("result")]
        public CResult Result;

        [JsonObject]
        public class CResult
        {
        }
    }
}
