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
    [Authorize]
    public class SalesController : Controller
    {
        private IQuarterlySalesUnitOfWork data { get; set; }
        public SalesController(IQuarterlySalesUnitOfWork unit) => data = unit;

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
            string message2 = CheckSalesYearAndQuarter(sales);
            if (!string.IsNullOrEmpty(message))
            {
                ModelState.AddModelError(nameof(sales.EmployeeId), message);
            }
            
            if (!string.IsNullOrEmpty(message2))
            {
                ModelState.AddModelError(nameof(sales.Quarter), message2);
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

        private string CheckSalesYearAndQuarter(Sales sale)
        {
            int hireQuarter = 0;
            Employee employee = data.Employees.Get(sale.EmployeeId);

            int hireYear = employee.DateOfHire.Value.Year;
            int hireMonth = employee.DateOfHire.Value.Month;
            switch (hireMonth)
            {
                case 1:
                case 2:
                case 3:
                    hireQuarter = 1;
                    break;
                case 4:
                case 5:
                case 6:
                    hireQuarter = 2;
                    break;
                case 7:
                case 8:
                case 9:
                    hireQuarter = 3;
                    break;
                case 10:
                case 11:
                case 12:
                    hireQuarter = 4;
                    break;
                default:
                    break;
            }

            if(sale.Year < hireYear)
            {
                return $"Sale year and quarter must be the hire date of {hireYear} Q{hireQuarter} or later.";
            }
            else if (sale.Year == hireYear && sale.Quarter < hireQuarter)
            {
                return $"Sale year and quarter must be the hire date of {hireYear} Q{hireQuarter} or later.";
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
