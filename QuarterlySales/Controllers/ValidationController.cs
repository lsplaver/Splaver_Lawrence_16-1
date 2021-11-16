using Microsoft.AspNetCore.Mvc;
using QuarterlySales.Models;
using QuarterlySales.Models.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterlySales.Controllers
{
    public class ValidationController : Controller
    {
        private SalesContext context { get; set; }

        public ValidationController(SalesContext ctx) => context = ctx;

        public JsonResult CheckEmployee(string firstName, string lastName, DateTime dateOfBirth)
        {
            Employee employee = new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth
            };

            string message = Validate.CheckEmployee(context, employee);
            if (string.IsNullOrEmpty(message))
            {
                return Json(true);
            }

            return Json(message);
        }

        public JsonResult CheckManager(int managerId, string firstName, string lastName, DateTime dateOfBirth)
        {
            Employee employee = new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                ManagerId = managerId
            };

            string message = Validate.CheckManagerEmployeeMatch(context, employee)
            if (string.IsNullOrEmpty(message))
            {
                return Json(true);
            }

            return Json(message);
        }

        public JsonResult CheckSales(int employeeId, int year, int quarter)
        {
            Sales sales = new Sales
            {
                EmployeeId = employeeId,
                Year = year,
                Quarter = quarter
            };

            string message = Validate.CheckSales(context, sales);
            if (string.IsNullOrEmpty(message))
            {
                return Json(true);
            }

            return Json(message);
        }
    }
}
