using System;
using Newtonsoft.Json;
using DAOLayer;

namespace AuthService
{
    [JsonObject]
    public class User
    {
        [JsonProperty("username")]
        public string Username;

        [JsonProperty("password")]
        public string Password;

        [JsonProperty("email")]
        public string Email;

        public AuthResult Register()
        {
            if(string.IsNullOrWhiteSpace(Username) || 
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(Email))
            {
                return new AuthResult()
                {
                    Result = false,
                    Message = "Lack of information"
                };
            }               


            //Query DB, if name exists, return error
            var connection = ConnectionFactory.Instance.GetMongoDBConnection();
            var userDAO = DAOFactory.Instance.GetUserDAO("webct", "_users");
            var result = userDAO.ReadAll(connection).Find(x=>string.Equals(Username, x.UserName));
            if (result != null)
            {
                return new AuthResult()
                {
                    Result = false,
                    Message = "UserName is used"
                };
            }


            //Insert DB
            userDAO.Create(connection, new DAOLayer.Entities.User()
            {
                Email = this.Email,
                UserName = this.Username,
                Password = this.Password,
            });

            return new AuthResult()
            {
                Result = true,
                Message = "Register succeed"
            };
        }

        public AuthResult SignIn()
        {
            //Query DB, if name and password match, create token
            var connection = ConnectionFactory.Instance.GetMongoDBConnection();
            var userDAO = DAOFactory.Instance.GetUserDAO("webct", "_users");
            var result = userDAO.ReadAll(connection).Find(x => (string.Equals(Email, x.Email) || string.Equals(Username, x.UserName)) && string.Equals(Password, x.Password));
            if (result == null)
            {
                return new AuthResult()
                {
                    Result = false,
                    Message = "UserName or password is wrong"
                };
            }

            this.Username = result.UserName;
            this.Email = result.Email;
            JWToken jwToken = JWToken.Create(this);


            //Return token
            return new AuthResult()
            {
                Result = true,
                Message = "Sign in succeed",
                Token = jwToken
            };
        }
    }
}
