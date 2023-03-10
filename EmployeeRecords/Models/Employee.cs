using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace EmployeeRecords.Models
{
    public class Employee
    {
        [Display(Name = "Employee ID")]
        public int employee_id { get; set; }

        [Display(Name = "Department")]
        [Required(ErrorMessage = "Please select Department")]
        public int Department_id { get; set; }

        public string Department_name { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please enter first name")]
        public string Firstname { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please enter last name")]
        public string Lastname { get; set; }

        [Display(Name = "Employee Code")]
        [Required(ErrorMessage = "Please enter employee code")]
        public string Empcode { get; set; }

        [Display(Name = "Date of Birth")]
        [Required(ErrorMessage = "Please select date of birth")]
        public DateTime? Dob { get; set; }
        public string strDob { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "Please enter city")]
        public string City { get; set; }

        [Display(Name = "Active Status")]
        public Boolean IsActive { get; set; }

        [Display(Name = "Created Date")]
        public DateTime? Created_at { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime? Updated_at { get; set; }

        public IEnumerable<Employee> Employees { get; set; }
    }
}