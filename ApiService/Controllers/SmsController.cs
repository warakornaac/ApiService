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
using Newtonsoft.Json;
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
        [Route("Post/Sms")]
        public async Task<string> Post([FromBody] SmsModels models)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://portal-otp.smsmkt.com/api/send-message");

            //var byteArray = Encoding.ASCII.GetBytes("6d0f2f7c7169cb0f215c23122c6b5bd2:DYKS1JPrGE1VmRjO");
            //request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            request.Headers.Add("api_key", "6d0f2f7c7169cb0f215c23122c6b5bd2");
            request.Headers.Add("secret_key", "DYKS1JPrGE1VmRjO");


            var json = JsonConvert.SerializeObject(new
            {
                phone = models.Phone,
                message = "รหัส OTP ของคุณคือ : " + models.Otp + " ref: " + models.Ref,
                sender = "Demo-SMS"
            });

            // สร้าง StringContent สำหรับ body ของคำขอ
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            request.Content = content;

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            String modelsJson = JsonConvert.SerializeObject(models);

            String lastId = _apiServerService.SaveApiResponse("Post/Sms", modelsJson.ToString(), models.User.ToString());
            _apiServerService.UpdateApiRespone(lastId, responseBody.ToString());

            //return responseBody;
            return responseBody;

        }


    }
}