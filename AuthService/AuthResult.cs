using Newtonsoft.Json;

namespace AuthService
{
    [JsonObject]
    public class AuthResult
    {
        [JsonProperty("result")]
        public bool Result;

        [JsonProperty("message")]
        public string Message;

        [JsonProperty("token")]
        public JWToken Token;
    }
}
