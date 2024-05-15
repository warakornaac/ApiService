using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using ApiService.Services;

namespace ApiService.Filters
{
    public class ApiKeyAuthorizeAttribute : AuthorizationFilterAttribute
    {
        //private const string ApiKeyHeaderName = "ApiKey";

        //public override void OnActionExecuting(HttpActionContext filterContext)
        //{
        //    var provider = filterContext.ControllerContext.Configuration.DependencyResolver.GetService(typeof(IApiKeyProvider)) as IApiKeyProvider;

        //    IEnumerable<string> apiKeys;
        //    filterContext.Request.Headers.TryGetValues(ApiKeyHeaderName, out apiKeys);

        //    // Check API key
        //    if (apiKeys == null || apiKeys.FirstOrDefault() != provider.GetApiKey())
        //    {
        //        filterContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
        //    }

        //    base.OnActionExecuting(filterContext);
        //}
        private const string ApiKeyHeaderName = "ApiKey";
        private static readonly string ApiKey = "iL0UCJtAwwq8nVjvUJoVkM9CjFhyycLp";
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Contains(ApiKeyHeaderName))
            {
                var apiKeyHeaderValue = actionContext.Request.Headers.GetValues(ApiKeyHeaderName).FirstOrDefault();
                if (apiKeyHeaderValue == ApiKey)
                {
                    return;
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, "Invalid API Key");
                }
            }
            else 
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, "Required Header API Key");
            }

        }
    }
}