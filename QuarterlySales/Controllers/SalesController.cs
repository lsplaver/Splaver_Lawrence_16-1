using Microsoft.AspNetCore.Mvc;
using QuarterlySales.Models;
using QuarterlySales.Models.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterlySales.Controllers
{
    public class SalesController : Controller
    {
        private QuarterlySalesUnitOfWork data { get; set; }
        public SalesController(SalesContext ctx) => data = new QuarterlySalesUnitOfWork(ctx);

        public IActionResult Index() => RedirectToAction("Index", "Home");

        [HttpGet]
        public ViewResult Add()
        {
            ViewBag.Employees = data.Employees.List(new QueryOptions<Employee> { OrderBy = e => e.FirstName });
            return View();
        }

        [HttpPost]
        public IActionResult Add(Sales sales)
        {
            string message = Validate.CheckSales(data, sales);
            if (!string.IsNullOrEmpty(message))
            {
                ModelState.AddModelError(nameof(sales.EmployeeId), message);
            }

            if (ModelState.IsValid)
            {
                data.Sales.Insert(sales);
                data.Save();
                TempData["message"] = $"Sales added";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Employees = data.Employees.List(new QueryOptions<Employee> { OrderBy = e => e.FirstName });
                return View();
            }
        }
    }
}
