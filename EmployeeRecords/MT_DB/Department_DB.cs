using EmployeeRecords.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeRecords.MT_DB
{
    public class Department_DB
    {
        private string connString = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();

        public List<SelectListItem> GetDepartments()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "----- Select -----", Value = "" });
            try
            {
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(connString);
                string query = "SELECT * FROM DEPARTMENT";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                con.Close();
                sda.Dispose();

                if(dt !=null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        list.Add(new SelectListItem { Text = dr["Department_name"].ToString(), Value = dr["Department_id"].ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Session["Error"] = ex.Message;
            }
            return list;
        }
    }
}