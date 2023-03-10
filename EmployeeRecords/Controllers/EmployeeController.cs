using EmployeeRecords.Models;
using EmployeeRecords.MT_DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeRecords.Controllers
{
    public class EmployeeController : Controller
    {

        // GET: Employee
        public ActionResult Index()
        {
            Employee emp = new Employee();
            Employee_DB emp_DB = new Employee_DB();
            Department_DB dep_DB=new Department_DB();
            emp.Employees = emp_DB.GetEmployees();
            ViewBag.departments = dep_DB.GetDepartments();
            return View(emp);
        }

        [HttpPost]
        public ActionResult CreateEmp(Employee model)
        {
            Department_DB dep_DB = new Department_DB();
            ViewBag.departments = dep_DB.GetDepartments();
            if (ModelState.IsValid)
            {
                Employee_DB employee_DB = new Employee_DB();
                bool status = employee_DB.CreateEmp(model);
                if (status)
                {
                    ViewBag.Message = "Employee created successfully!";
                }
                else
                {
                    ViewBag.Message = "Something went wrong! Please submit form again.";
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult GetEmployee(int ID)
        {
            Employee emp = new Employee();
            Employee_DB employee_DB=new Employee_DB();
            emp = employee_DB.GetEmployee(ID);
            return Json(emp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditEmp(Employee model)
        {
            Department_DB dep_DB = new Department_DB();
            ViewBag.departments = dep_DB.GetDepartments();
            if (ModelState.IsValid)
            {
                Employee_DB employee_DB = new Employee_DB();
                bool status = employee_DB.EditEmp(model);
                if (status)
                {
                    ViewBag.Message = "Employee created successfully!";
                }
                else
                {
                    ViewBag.Message = "Something went wrong! Please submit form again.";
                }
            }
            return RedirectToAction("Index");
        }
    }
}