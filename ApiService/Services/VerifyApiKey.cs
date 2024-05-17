using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ApiService.Services
{
    public class VerifyApiKey
    {
        public string CheckApiKey(string username, string password, string apiKey)
        {
            var storedResult = string.Empty;
            var flagResult = string.Empty;
            var txtResult = string.Empty;
            var txtRespond = "Y";
            var connectionString = ConfigurationManager.ConnectionStrings["APIDB_ConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                var cmd = new SqlCommand("P_Verify_Key", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@ApiKey", apiKey);

                SqlParameter p = new SqlParameter("@OutGenstatus", SqlDbType.NVarChar, 100);
                p.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p);
                cmd.ExecuteNonQuery();
                storedResult = cmd.Parameters["@OutGenstatus"].Value.ToString();
                if (!string.IsNullOrEmpty(storedResult)) {
                    string[] fullResultStored = storedResult.Split('|');
                    flagResult = fullResultStored[0];
                    txtResult = fullResultStored[1];
                    if (flagResult != "ApiKey") { 
                        txtRespond = txtResult;
                    }
                }
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                txtRespond = ex.ToString();
            }
            conn.Close();
            return txtRespond;
        }
    }
}