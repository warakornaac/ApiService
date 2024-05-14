using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.DirectoryServices;
using System.Configuration;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ApiService.Controllers;
using RouteAttribute = System.Web.Http.RouteAttribute;
using AuthorizeAttribute = System.Web.Http.AuthorizeAttribute;
using ApiService.ApiKeyAuthorizeAttribute;

namespace ApiService.Controllers
{

    public class AuthenSalesController : ApiController
    {
        //[Route("Post/AuthenSale")]
        //public IHttpActionResult Post(string username, string password)
        //{
        //    string errorMessage = "Success";
        //    string fullnameAd = string.Empty;
        //    //call function success
        //    var okresp = new HttpResponseMessage(HttpStatusCode.OK)
        //    {
        //        ReasonPhrase = "Success"
        //    };
        //    if (string.IsNullOrEmpty(username)) {
        //        errorMessage = "Username not null";
        //    }
        //    if (string.IsNullOrEmpty(password)) {
        //        errorMessage = "Password not null";
        //    }
        //    if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password)) //have user/pass
        //    {
        //        try
        //        {
        //            DirectoryEntry directionEntry = new DirectoryEntry("LDAP://ADSRV2016-01/dc=Automotive,dc=com", username, password);
        //            if (directionEntry != null)
        //            {
        //                DirectorySearcher search = new DirectorySearcher(directionEntry);
        //                search.Filter = "(SAMAccountName=" + username + ")";
        //                SearchResult result = search.FindOne();
        //                if (result != null)
        //                {
        //                    DirectoryEntry userEntry = result.GetDirectoryEntry();
        //                    if (userEntry != null)
        //                    {
        //                        fullnameAd = userEntry.Properties["Name"].Value.ToString();
        //                    }
        //                }
        //                else 
        //                {
        //                    errorMessage = result.ToString();
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            errorMessage = ex.Message.ToString();
        //        }
        //    }
        //    DataRespond dataRes = new DataRespond();
        //    dataRes.statusCode = Convert.ToInt32(okresp.StatusCode);
        //    dataRes.errorMessage = errorMessage;
        //    dataRes.fullnameSales = fullnameAd;
        //    //string json = JsonConvert.SerializeObject(dataRes);
        //    //return json;
        //    return Json(dataRes);
        //}

        [Route("Post/AuthenSale")]
        public string Get()
        {
            return string.Format("Hello, {0}", User.Identity.Name);
        }
    }

    public class DataRespond
    {
        public int statusCode { get; set; }
        public string errorMessage { get; set; }
        public string fullnameSales { get; set; }
    }
}
