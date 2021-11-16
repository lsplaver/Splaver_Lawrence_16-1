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
        private SalesContext context { get; set; }

        public SalesController(SalesContext ctx) => context = ctx;

        public IActionResult Index() => RedirectToAction("Index", "Home");

        [HttpGet]
        public ViewResult Add()
        {
            ViewBag.Employees = context.Employees.OrderBy(e => e.LastName).ThenBy(e => e.FirstName).ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Add(Sales sales)
        {
            string message = Validate.CheckSales(context, sales);
            if (!string.IsNullOrEmpty(message))
            {
                ModelState.AddModelError(nameof(sales.EmployeeId), message);
            }

            if (ModelState.IsValid)
            {
                context.Sales.Add(sales);
                context.SaveChanges();
                TempData["message"] = $"Sales added";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Employees = context.Employees.OrderBy(e => e.LastName).ThenBy(e => e.FirstName).ToList();
                return View();
            }
        }
    }
}
