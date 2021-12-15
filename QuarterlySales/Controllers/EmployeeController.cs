using Microsoft.AspNetCore.Mvc;
using QuarterlySales.Models;
using QuarterlySales.Models.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterlySales.Controllers
{
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
            if (!string.IsNullOrEmpty(message))
            {
                ModelState.AddModelError(nameof(Employee.DateOfBirth), message);
            }

            message = Validate.CheckManagerEmployeeMatch(data, employee);
            if (!string.IsNullOrEmpty(message))
            {
                ModelState.AddModelError(nameof(Employee.ManagerId), message);
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
    }
}
