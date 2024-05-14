using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ApiService.ApiKeyAuthorizeAttribute
{
    public class ApiKeyAuthorizeAttribute : AuthorizationFilterAttribute
    {
        private const string API_KEY_HEADER = "X-API-KEY";
        private static readonly string API_KEY = "1234abcd"; // Replace with your actual API key

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Contains(API_KEY_HEADER))
            {
                var apiKeyHeaderValue = actionContext.Request.Headers.GetValues(API_KEY_HEADER).FirstOrDefault();
                if (apiKeyHeaderValue == API_KEY)
                {
                    // API key is valid, proceed with the action
                    return;
                }
            }

            // API key is missing or invalid, deny access
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, "Invalid API Key");
        }
    }
}