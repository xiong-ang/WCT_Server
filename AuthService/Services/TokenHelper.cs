﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthService
{
    class TokenHelper
    {
        /// <summary>
        /// Use the below code to generate symmetric Secret Key
        ///     var hmac = new HMACSHA256();
        ///     var key = Convert.ToBase64String(hmac.Key);
        /// </summary>
        private const string Secret = "UMnuMfbhfeyksqfCoqTch5i5e0GWxLThO0K+aNHZ0KosWkKBlGVsL38NgFuvrx2HfIn5uJDnxstIgBB6ULIMbA==";

        /// <summary>
        /// 此方法用来生成 Token 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="expireMinutes"></param>
        /// <returns></returns>
        public static string GenerateToken(string username, int expireMinutes = 120)
        {
            var tokenDescriptor = new SecurityTokenDescriptor // 创建一个 Token 的原始对象
            {
                Subject = new ClaimsIdentity(new[] // Token的身份证，类似一个人可以有身份证，户口本
                        {
                            new Claim(ClaimTypes.Name, username) // 可以创建多个
                        }),

                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(expireMinutes)), // Token 有效期

                // 生成一个Token证书，第一个参数是密码，第二个参数是编码方式
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Convert.FromBase64String(Secret)), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler(); // 创建一个JwtSecurityTokenHandler类用来生成Token
            var stoken = tokenHandler.CreateToken(tokenDescriptor); // 生成一个编码后的token对象实例
            var token = tokenHandler.WriteToken(stoken); // 生成token字符串，给前端使用
            return token;
        }

        /// <summary>
        /// 此方法用解码字符串token，并返回秘钥的信息对象
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler(); // 创建一个JwtSecurityTokenHandler类，用来后续操作
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken; // 将字符串token解码成token对象
                if (jwtToken == null)
                    return null;


                var validationParameters = new TokenValidationParameters() // 生成验证token的参数
                {
                    RequireExpirationTime = true, // token是否包含有效期
                    ValidateIssuer = false, // 验证秘钥发行人，如果要验证在这里指定发行人字符串即可
                    ValidateAudience = false, // 验证秘钥的接受人，如果要验证在这里提供接收人字符串即可
                    IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(Secret)) // 生成token时的安全秘钥
                };

                SecurityToken securityToken; // 接受解码后的token对象
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
                return principal; // 返回秘钥的主体对象，包含秘钥的所有相关信息
            }

            catch (Exception)
            {
                return null;
            }
        }
    }
}
