using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthService
{
    [JsonObject]
    public class JWToken
    {
        [JsonProperty("token")]
        public string Token;

        [JsonProperty("expire")]
        public int Expire = 120;

        public static JWToken Create(User user)
        {

            return new JWToken() { Token = TokenHelper.GenerateToken(user.Username) };
        }

        public static bool Validate(string token, out string username)
        {
            username = null;
            if (string.IsNullOrWhiteSpace(token)) return false;


            var simplePrinciple = TokenHelper.GetPrincipal(token); // 调用自定义的GetPrincipal获取Token的信息对象
            var identity = simplePrinciple?.Identity as ClaimsIdentity; // 获取主声明标识
            if (identity == null) return false;
            if (!identity.IsAuthenticated) return false;


            var userNameClaim = identity.FindFirst(ClaimTypes.Name); // 获取声明类型是ClaimTypes.Name的第一个声明
            username = userNameClaim?.Value; // 获取声明的名字，也就是用户名
            if (string.IsNullOrEmpty(username)) return false;

            return true;
        }
    }
}
