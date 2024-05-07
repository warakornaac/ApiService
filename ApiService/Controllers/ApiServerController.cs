using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using ApiService.Models;
using Newtonsoft.Json;

namespace ApiService.Controllers
{
    //public interface IApiServerService
    //{
    //    void SaveApiResponse(string method, string inservice, string user);
    //    void UpdateApiRespone(string inservice, string respon);
    //}
    public class ApiServerController : Controller
    {
        public string SaveApiResponse(string method, string inservice, string user)
        {
            var outReturn = "";
            var connectionString = ConfigurationManager.ConnectionStrings["MobileOrder_ConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                var cmd = new SqlCommand("P_Save_Apiservice_log", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@method", method);
                cmd.Parameters.AddWithValue("@inservice", inservice);
                cmd.Parameters.AddWithValue("@user", user);
                SqlParameter p = new SqlParameter("@outGenstatus", SqlDbType.NVarChar, 100);
                p.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p);

                cmd.ExecuteNonQuery();
                outReturn = cmd.Parameters["@outGenstatus"].Value.ToString();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString() + "Error_SAVE");
            }
            conn.Close();
            return outReturn;
        }
        public void UpdateApiRespone(string id, string respon)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MobileOrder_ConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                var cmd = new SqlCommand("P_Update_Apiservice_log", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@response", respon);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString() + "Error_UPDATE");
            }
            conn.Close();
        }
    }
    //public class ApiServerInjectionController : ApiController
    //{
    //    private readonly IApiServerService _apiServerService;

    //    public ApiServerInjectionController(IApiServerService apiServerService)
    //    {
    //        _apiServerService = apiServerService;
    //    }

    //}
}
