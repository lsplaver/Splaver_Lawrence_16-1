using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuarterlySales.Models;
using QuarterlySales.Models.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterlySales.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EmployeeController : Controller
    {
        private Repository<Employee> data { get; set; }
        public EmployeeController(SalesContext ctx) => data = new Repository<Employee>(ctx);

        public IActionResult Index() => RedirectToAction("Index", "Home");

        [HttpGet]
        public ViewResult Add()
        {
            ViewBag.Employees = data.List(new QueryOptions<Employee> { OrderBy = e => e.FirstName });
            return View();
        }

        [HttpPost]
        public IActionResult Add(Employee employee)
        {
            string message = Validate.CheckEmployee(data, employee);
            string message2 = CheckManagerHireDate(employee);
            if (!string.IsNullOrEmpty(message))
            {
                ModelState.AddModelError(nameof(Employee.DateOfBirth), message);
            }

            message = Validate.CheckManagerEmployeeMatch(data, employee);
            if (!string.IsNullOrEmpty(message))
            {
                ModelState.AddModelError(nameof(Employee.ManagerId), message);
            }

            if (!string.IsNullOrEmpty(message2))
            {
                ModelState.AddModelError(nameof(Employee.ManagerId), message2);
            }

            if (ModelState.IsValid)
            {
                data.Insert(employee);
                data.Save();
                TempData["message"] = $"Employee {employee.FullName} added";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Employees = data.List(new QueryOptions<Employee> { OrderBy = e => e.FirstName });
                return View();
            }
        }
        private string CheckManagerHireDate(Employee employee)
        {
            Employee manager = data.Get(employee.ManagerId);
            
            if (employee.DateOfHire < manager.DateOfHire)
            {
                return "Manager must be hired before new employee";
            }
            else
            {
                return string.Empty;
            }
            //int hireYear = employee.DateOfHire.Value.Year;
            //int hireMonth = employee.DateOfHire.Value.Month;
            //switch (hireMonth)
            //{
            //    case 1:
            //    case 2:
            //    case 3:
            //        hireQuarter = 1;
            //        break;
            //    case 4:
            //    case 5:
            //    case 6:
            //        hireQuarter = 2;
            //        break;
            //    case 7:
            //    case 8:
            //    case 9:
            //        hireQuarter = 3;
            //        break;
            //    case 10:
            //    case 11:
            //    case 12:
            //        hireQuarter = 4;
            //        break;
            //    default:
            //        break;
            //}
            //if (sale.Year < hireYear && sale.Quarter < hireQuarter)
            //{
            //    return $"Sale quarter must be {hireYear} Q{hireQuarter} or later";
            //}
            //else if (sale.Year >= hireYear && sale.Quarter < hireQuarter)
            //{
            //    return $"Sale quarter must be {hireYear} Q{hireQuarter} or later.";
            //}
            //else
            //{
            //    return string.Empty;
            //}
        }
    }
}
