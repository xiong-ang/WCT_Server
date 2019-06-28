using AuthService;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace CTServer.Controllers
{
    public class AuthFilterAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            //如果用户方位的Action带有AllowAnonymousAttribute，则不进行授权验证
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()) return;



            var tokenElements = actionContext.Request.Headers.FirstOrDefault(x => string.Equals(x.Key, "Authorization")).Value;
            if(tokenElements != null)
            {
                var token = tokenElements.ElementAt(0);
                string temp;
                if (JWToken.Validate(token, out temp)) return;
            }

            actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new HttpError("Authorization failed"));
        }
    }
}