using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiService.Services : IApiKeyProvider
{
    private const string ApiKey = "iL0UCJtAwwq8nVjvUJoVkM9CjFhyycLp";
    public class SimpleApiKeyProvider
    {
            return ApiKey;
    }
}