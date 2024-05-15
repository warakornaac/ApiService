using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiService.Services;

namespace ApiService.Services
{
    public interface IApiKeyProvider
    {
        string GetApiKey();
    }
}