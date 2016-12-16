using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http;
using System.Net;

namespace HikeHandlerWebApi.Filters
{
    public class CustomAuthenticationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            KeyValuePair<string, IEnumerable<string>> apiKey = actionContext.Request.Headers.FirstOrDefault(x => x.Key == "api-key");

            if (apiKey.Key.Equals("api-key") && apiKey.Value.ToList().Count == 1 && apiKey.Value.ToList()[0].Equals("almafa"))
            {
                // authorized, nothing to do
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
            
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
        }
    }
}