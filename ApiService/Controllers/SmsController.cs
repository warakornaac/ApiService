using ApiService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using ApiService.Controllers;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace ApiService.Controllers
{
    public class SmsController : ApiController
    {
        private readonly ApiServerController _apiServerService;

        // ตัวอย่างการสร้าง constructor ที่ไม่มีพารามิเตอร์
        public SmsController()
        {
            // สร้าง instance ของ IApiServerService แบบไหนก็ได้ หรือไม่ต้องสร้างก็ได้
            _apiServerService = new ApiServerController(); // หรือใช้วิธี dependency injection อื่น ๆ
        }

        public SmsController(ApiServerController apiServerService)
        {
            _apiServerService = apiServerService;
        }

        //POST: Sms
        [Route("Post/SendSms")]
        public async Task<string> Post([FromBody] SmsModels models)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://portal-otp.smsmkt.com/api/send-message");
            request.Headers.Add("api_key", "ea9ccaa9aff6e0198be2e0185c3caea2");
            request.Headers.Add("secret_key", "QBFo5Es5A3OI5xYS");

            var json = JsonConvert.SerializeObject(new
            {
                phone = models.Phone,
                message = models.Text,
                sender = "TAC-AAC"
            });

            // สร้าง StringContent สำหรับ body ของคำขอ
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            request.Content = content;

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            String modelsJson = JsonConvert.SerializeObject(models);

            String lastId = _apiServerService.SaveApiResponse("Post/SendSms", modelsJson.ToString(), models.User.ToString());
            _apiServerService.UpdateApiRespone(lastId, responseBody.ToString());

            return responseBody;
        }
    }
}