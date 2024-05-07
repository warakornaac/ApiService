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
using System.Net;

namespace ApiService.Controllers
{
    public class LineController : ApiController
    {
        private readonly ApiServerController _apiServerService;

        // ตัวอย่างการสร้าง constructor ที่ไม่มีพารามิเตอร์
        public LineController()
        {
            // สร้าง instance ของ IApiServerService แบบไหนก็ได้ หรือไม่ต้องสร้างก็ได้
            _apiServerService = new ApiServerController(); // หรือใช้วิธี dependency injection อื่น ๆ
        }
        //POST: Sms
        [Route("Post/PushMessage")]
        public async Task<string> Post([FromBody] SendDeliveryModels models)
        {
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var HttpClient = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.line.me/v2/bot/message/push");

                request.Headers.Add("Authorization", "Bearer vuBOI4p+ni6h4cUy4xRoad5D8GzqheZ7b8pyxEmcUtBq4NEaJtXPjCwnyRWvn75v5FMJH/0h6U7SagkcM/UVFLdGKdfz0bGk7mHbueizQwyJ7vDTs3ta8K2DI+h5y/xFpd6eHawQvIvREHXjXLBRewdB04t89/1O/w1cDnyilFU=");
                var jsonText = "{\r\n    \"to\": \"" + models.Uid + "\",\r\n    \"messages\": [\r\n  {\r\n \"type\": \"flex\",\r\n \"altText\": \"แจ้งเดือนสถานะการส่ง\",\r\n  \"contents\": {\r\n  \"type\": \"bubble\",\r\n  \"header\": {\r\n                    \"type\": \"box\",\r\n                    \"layout\": \"vertical\",\r\n                    \"contents\": [\r\n                        {\r\n                            \"type\": \"text\",\r\n                            \"text\": \"สถานะการส่งสินค้า\",\r\n                            \"weight\": \"bold\",\r\n                            \"color\": \"#FFFFFF\",\r\n                            \"margin\": \"none\",\r\n                            \"position\": \"relative\",\r\n                            \"align\": \"center\",\r\n                            \"style\": \"normal\"\r\n                        }\r\n                    ],\r\n                    \"maxHeight\": \"40px\",\r\n                    \"paddingTop\": \"md\",\r\n                    \"flex\": 0,\r\n                    \"paddingBottom\": \"md\"\r\n                },\r\n                \"body\": {\r\n                    \"type\": \"box\",\r\n                    \"layout\": \"vertical\",\r\n                    \"contents\": [\r\n                        {\r\n                            \"type\": \"box\",\r\n                            \"layout\": \"vertical\",\r\n                            \"margin\": \"none\",\r\n                            \"spacing\": \"sm\",\r\n                            \"contents\": [\r\n                                {\r\n                                    \"type\": \"box\",\r\n                                    \"layout\": \"baseline\",\r\n                                    \"spacing\": \"sm\",\r\n                                    \"contents\": [\r\n                                        {\r\n                                            \"type\": \"text\",\r\n                                            \"text\": \"เลขที่ออเดอร์:\",\r\n                                            \"color\": \"#aaaaaa\",\r\n                                            \"size\": \"xs\",\r\n                                            \"weight\": \"bold\",\r\n                                            \"wrap\": true,\r\n                                            \"margin\": \"none\",\r\n                                            \"flex\": 3\r\n                                        },\r\n                                        {\r\n                                            \"type\": \"text\",\r\n                                            \"text\": \"" + models.Docno + "\",\r\n                                            \"wrap\": true,\r\n                                            \"color\": \"#666666\",\r\n                                            \"size\": \"sm\",\r\n                                            \"flex\": 6,\r\n                                            \"weight\": \"bold\"\r\n                                        }\r\n                                    ]\r\n                                },\r\n                                {\r\n                                    \"type\": \"box\",\r\n                                    \"layout\": \"baseline\",\r\n                                    \"spacing\": \"sm\",\r\n                                    \"contents\": [\r\n                                        {\r\n                                            \"type\": \"text\",\r\n                                            \"text\": \"วันที่สั่งซื้อ:\",\r\n                                            \"color\": \"#aaaaaa\",\r\n                                            \"size\": \"xs\",\r\n                                            \"flex\": 1,\r\n                                            \"weight\": \"bold\",\r\n                                            \"wrap\": false\r\n                                        },\r\n                                        {\r\n                                            \"type\": \"text\",\r\n                                            \"text\": \"" + models.Docdate + "\",\r\n                                            \"wrap\": false,\r\n                                            \"color\": \"#666666\",\r\n                                            \"size\": \"sm\",\r\n                                            \"flex\": 3,\r\n                                            \"weight\": \"regular\"\r\n                                        }\r\n                                    ]\r\n                                },\r\n                                {\r\n                                    \"type\": \"box\",\r\n                                    \"layout\": \"baseline\",\r\n                                    \"spacing\": \"sm\",\r\n                                    \"contents\": [\r\n                                        {\r\n                                            \"type\": \"text\",\r\n                                            \"text\": \"ร้านค้า:\",\r\n                                            \"color\": \"#aaaaaa\",\r\n                                            \"size\": \"xs\",\r\n                                            \"flex\": 2,\r\n                                            \"weight\": \"bold\",\r\n                                            \"wrap\": false\r\n                                        },\r\n                                        {\r\n                                            \"type\": \"text\",\r\n                                            \"text\": \"" + models.Cusname + "\",\r\n                                            \"wrap\": false,\r\n                                            \"color\": \"#666666\",\r\n                                            \"size\": \"sm\",\r\n                                            \"flex\": 9,\r\n                                            \"weight\": \"regular\"\r\n                                        }\r\n                                    ]\r\n                                }\r\n                            ]\r\n                        },\r\n                        {\r\n                            \"type\": \"separator\",\r\n                            \"margin\": \"md\"\r\n                        },\r\n                        {\r\n                            \"type\": \"box\",\r\n                            \"layout\": \"vertical\",\r\n                            \"contents\": [\r\n                                {\r\n                                    \"type\": \"box\",\r\n                                    \"layout\": \"baseline\",\r\n                                    \"contents\": [\r\n                                        {\r\n                                            \"type\": \"text\",\r\n                                            \"text\": \"สถานะจัดส่ง:\",\r\n                                            \"weight\": \"bold\",\r\n                                            \"color\": \"#aaaaaa\",\r\n                                            \"size\": \"xs\",\r\n                                            \"margin\": \"none\",\r\n                                            \"flex\": 1\r\n                                        },\r\n                                        {\r\n                                            \"type\": \"text\",\r\n                                            \"text\": \"" + models.Delivery + "\",\r\n                                            \"margin\": \"none\",\r\n                                            \"weight\": \"bold\",\r\n                                            \"size\": \"sm\",\r\n                                            \"color\": \"#04B431\",\r\n                                            \"flex\": 2\r\n                                        }\r\n                                    ]\r\n                                }\r\n                            ],\r\n                            \"margin\": \"md\"\r\n                        }\r\n                    ]\r\n                },\r\n                \"styles\": {\r\n                    \"header\": {\r\n                        \"backgroundColor\": \"#44bcd8\",\r\n                        \"separator\": true\r\n                    },\r\n                    \"hero\": {\r\n                        \"backgroundColor\": \"#44bcd8\"\r\n                    }\r\n                }\r\n            }\r\n        }\r\n    ]\r\n}";
                var content = new StringContent(jsonText, System.Text.Encoding.UTF8, "application/json");
                request.Content = content;
                var response = await HttpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                //end send api
                //keep log
                String modelsJson = JsonConvert.SerializeObject(models);
                String lastId = _apiServerService.SaveApiResponse("Post/PushMessage", modelsJson.ToString(), models.User.ToString());
                _apiServerService.UpdateApiRespone(lastId, responseBody.ToString());
                return responseBody;
            }
            catch (Exception ex)
            {
                // Handle the exception
                //return ex.Message;
                // throw new HttpResponseException(HttpStatusCode.InternalServerError);
                var res = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                res.Content = new StringContent(ex.Message);
                throw new HttpResponseException(res);

            }

        }

    }
}
