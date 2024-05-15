using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiService.Services;

namespace ApiService.Services
{
    public class SimpleApiKeyProvider : IApiKeyProvider
    {
        string ApiKey = "iL0UCJtAwwq8nVjvUJoVkM9CjFhyycLp";
        public string GetApiKey()
        {
            return ApiKey;
        }
    }
}