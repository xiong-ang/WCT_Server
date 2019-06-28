using System.Web.Http;
using AuthService;

namespace CTServer.Controllers
{
    /// <summary>
    /// Controller for use authorization
    /// </summary>
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        [HttpPost]
        [Route("new")]
        public AuthResult Register([FromBody]User user)
        {
            try
            {
                return user.Register();
            }
            catch (System.Exception)
            {
                return new AuthResult()
                {
                    Result = false,
                    Message = "Post Data Error"
                };
            }
            
        }


        [HttpPost]
        [Route("verify")]
        public AuthResult SignIn([FromBody]User user)
        {
            try
            {
                return user.SignIn();
            }
            catch (System.Exception)
            {
                return new AuthResult()
                {
                    Result = false,
                    Message = "Post Data Error"
                };
            }
        }    
    }
}
