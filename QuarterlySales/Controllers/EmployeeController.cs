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
        // private SalesContext context { get; set; }

        private Repository<Employee> data { get; set; }
        public EmployeeController(SalesContext ctx) => data = new Repository<Employee>(ctx);

        public IActionResult Index() => RedirectToAction("Index", "Home");

        [HttpGet]
        public ViewResult Add()
        {
            // ViewBag.Employees = context.Employees.OrderBy(e => e.LastName).ThenBy(e => e.FirstName).ToList();
            ViewBag.Employees = data.List(new QueryOptions<Employee> { OrderBy = e => e.FirstName });
            return View();
        }

        [HttpPost]
        public IActionResult Add(Employee employee)
        {
            string message = Validate.CheckEmployee(/*context*/ data, employee);
            if (!string.IsNullOrEmpty(message))
            {
                ModelState.AddModelError(nameof(Employee.DateOfBirth), message);
            }

            message = Validate.CheckManagerEmployeeMatch(/*context*/ data, employee);
            if (!string.IsNullOrEmpty(message))
            {
                ModelState.AddModelError(nameof(Employee.ManagerId), message);
            }

            if (ModelState.IsValid)
            {
                // context.Employees.Add(employee);
                data.Insert(employee);
                // context.SaveChanges();
                data.Save();
                TempData["message"] = $"Employee {employee.FullName} added";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //ViewBag.Employees = context.Employees.OrderBy(e => e.LastName).ThenBy(e => e.FirstName).ToList();
                ViewBag.Employees = data.List(new QueryOptions<Employee> { OrderBy = e => e.FirstName });
                return View();
            }
        }
    }
}
