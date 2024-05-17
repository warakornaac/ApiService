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
        string txtResult = "";
        private const string ApiKeyHeaderNameUsername = "Username";
        private const string ApiKeyHeaderNamePassword = "Password";
        private const string ApiKeyHeaderNameApiKey = "ApiKey";
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var headers = actionContext.Request.Headers;
            //checy username
            if (headers.Contains(ApiKeyHeaderNameUsername))
            {
                //check password
                if (headers.Contains(ApiKeyHeaderNamePassword))
                {
                    //check apiKey
                    if (headers.Contains(ApiKeyHeaderNameApiKey))
                    {
                        var apiKeyHeaderUsernameValue = headers.GetValues(ApiKeyHeaderNameUsername).FirstOrDefault();
                        var apiKeyHeaderPasswordValue = headers.GetValues(ApiKeyHeaderNamePassword).FirstOrDefault();
                        var apiKeyHeaderApiValue = headers.GetValues(ApiKeyHeaderNameApiKey).FirstOrDefault();
                        var verifyApiKey = new VerifyApiKey().CheckApiKey(apiKeyHeaderUsernameValue, apiKeyHeaderPasswordValue, apiKeyHeaderApiValue);
                        //api pass
                        if (verifyApiKey == "Y")
                        {
                            return;
                        }
                        else  //api not pass
                        {
                            txtResult = verifyApiKey;
                        }
                    }
                    else
                    {
                        txtResult = "Required Header ApiKey";
                    }
                }
                else
                {
                    txtResult = "Required Header Password";
                }
            }
            else
            {
                txtResult = "Required Header Username";
            }
            if (txtResult != "") {
                var dataResult = new {
                    statusCode = HttpStatusCode.Forbidden,
                    errorMessage = txtResult,
                    result = ""
                };
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, dataResult);
            }
        }
    }
}