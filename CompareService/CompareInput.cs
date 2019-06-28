using Newtonsoft.Json;

namespace CompareService
{
    [JsonObject]
    public class CompareInput
    {
        [JsonProperty("projectname")]
        public string ProjectName;

        [JsonProperty("file1")]
        public string FileName1;

        [JsonProperty("file2")]
        public string FileName2;

        [JsonProperty("config")]
        public CompareConfig Config;

        [JsonObject]
        public class CompareConfig
        {
            [JsonProperty("isIncludeAlltags")]
            public bool isIncludeAlltags;

            [JsonProperty("isIncludeTagDataValuesInCompare")]
            public bool isIncludeTagDataValuesInCompare;

            [JsonProperty("isIncludeConstantTagDataValuesInCompare")]
            public bool isIncludeConstantTagDataValuesInCompare;

            [JsonProperty("isFilterTags")]
            public bool isFilterTags;

            [JsonProperty("isIncludeDescriptionsInCompare")]
            public bool isIncludeDescriptionsInCompare;
        }
    }
}
