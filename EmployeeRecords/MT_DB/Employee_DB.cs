using EmployeeRecords.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;
using Microsoft.Ajax.Utilities;

namespace EmployeeRecords.MT_DB
{
    public class Employee_DB
    {
        private string connString = ConfigurationManager.ConnectionStrings["dbconnection"].ToString();

        public List<Employee> GetEmployees()
        {
            List<Employee> list = new List<Employee>();
            try
            {
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(connString);
                string query = "SELECT E.EMPLOYEE_ID, E.FIRSTNAME, E.LASTNAME, E.EMPCODE, E.DOB, E.CITY, E.IS_ACTIVE, E.CREATED_AT, E.UPDATED_AT, D.DEPARTMENT_NAME FROM EMPLOYEE E JOIN DEPARTMENT D ON E.DEPARTMENT_ID=D.DEPARTMENT_ID";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                con.Close();
                sda.Dispose();

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Employee emp = new Employee();
                        emp.employee_id = Convert.ToInt32(dr["EMPLOYEE_ID"]);
                        emp.Firstname = dr["FIRSTNAME"].ToString().Trim();
                        emp.Lastname = dr["LASTNAME"].ToString().Trim();
                        emp.Empcode = dr["EMPCODE"].ToString().Trim();
                        emp.Dob = !string.IsNullOrWhiteSpace(Convert.ToString(dr["DOB"])) ? ((DateTime?)dr["DOB"]) : null;
                        emp.City = dr["CITY"].ToString();
                        emp.Department_name = dr["DEPARTMENT_NAME"].ToString();
                        emp.IsActive = Convert.ToBoolean(dr["IS_ACTIVE"]);
                        emp.Created_at = !string.IsNullOrWhiteSpace(Convert.ToString(dr["CREATED_AT"])) ? ((DateTime?)dr["CREATED_AT"]) : null;
                        emp.Updated_at = !string.IsNullOrWhiteSpace(Convert.ToString(dr["UPDATED_AT"])) ? ((DateTime?)dr["UPDATED_AT"]) : null;
                        
                        list.Add(emp);
                    }
                }
            }
            catch(Exception ex)
            {
                HttpContext.Current.Session["errMsg"] = ex.Message;
            }
            return list;
        }

        public bool CreateEmp(Employee emp)
        {
            bool result = false;
            try
            {
                if (emp != null)
                {
                    SqlConnection con = new SqlConnection(connString);
                    string query = $"INSERT INTO EMPLOYEE(FIRSTNAME, LASTNAME, EMPCODE, DOB, CITY, DEPARTMENT_ID) VALUES('{emp.Firstname}', '{emp.Lastname}', '{emp.Empcode}', CAST('{emp.Dob}' AS DATETIME), '{emp.City}', {emp.Department_id})";
                    
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    int status = cmd.ExecuteNonQuery();
                    if (status > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Session["errMsg"] = ex.Message;
            }
            return result;
        }

        public Employee GetEmployee(int empID)
        {
            Employee emp = new Employee();
            try
            {
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(connString);
                string query = $"SELECT E.FIRSTNAME, E.LASTNAME, E.EMPCODE, E.DOB, E.CITY, E.IS_ACTIVE, E.CREATED_AT, E.UPDATED_AT, E.DEPARTMENT_ID FROM EMPLOYEE E WHERE E.EMPLOYEE_ID={empID}";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                con.Close();
                sda.Dispose();

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        emp.employee_id = empID;
                        emp.Firstname = dr["FIRSTNAME"].ToString().Trim();
                        emp.Lastname = dr["LASTNAME"].ToString().Trim();
                        emp.Empcode = dr["EMPCODE"].ToString().Trim();
                        emp.strDob = !string.IsNullOrWhiteSpace(Convert.ToString(dr["DOB"])) ? string.Format("{0:dd/MM/yyyy}", (DateTime?)dr["DOB"]) : string.Empty;
                        emp.City = dr["CITY"].ToString();
                        emp.Department_id = Convert.ToInt32(dr["DEPARTMENT_ID"]);
                        emp.IsActive = Convert.ToBoolean(dr["IS_ACTIVE"]);
                        emp.Created_at = !string.IsNullOrWhiteSpace(Convert.ToString(dr["CREATED_AT"])) ? ((DateTime?)dr["CREATED_AT"]) : null;
                        emp.Updated_at = !string.IsNullOrWhiteSpace(Convert.ToString(dr["UPDATED_AT"])) ? ((DateTime?)dr["UPDATED_AT"]) : null;
                    }
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Session["errMsg"] = ex.Message;
            }
            return emp;
        }

        public bool EditEmp(Employee emp)
        {
            bool result = false;
            try
            {
                if (emp != null)
                {
                    SqlConnection con = new SqlConnection(connString);
                    string dob = emp.Dob?.ToString("yyyyMMdd");
                    string query = $"UPDATE EMPLOYEE SET FIRSTNAME='{emp.Firstname}', LASTNAME='{emp.Lastname}', EMPCODE='{emp.Empcode}', DOB='{dob}', CITY='{emp.City}', DEPARTMENT_ID={emp.Department_id}, UPDATED_AT='{DateTime.Now}' WHERE EMPLOYEE_ID={emp.employee_id}";

                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    int status = cmd.ExecuteNonQuery();
                    if (status > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Session["errMsg"] = ex.Message;
            }
            return result;
        }
    }
}