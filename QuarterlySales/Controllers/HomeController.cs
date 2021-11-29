using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuarterlySales.Models;
using Microsoft.EntityFrameworkCore;

namespace QuarterlySales.Controllers
{
    public class HomeController : Controller
    {
        private QuarterlySalesUnitOfWork data { get; set; }
        public HomeController(SalesContext ctx) => data = new QuarterlySalesUnitOfWork(ctx);

        [HttpGet]
        public ViewResult Index(SalesGridDTO values)
        {
            var builder = new SalesGridBuilder(HttpContext.Session, values, defaultSortField: nameof(Sales.Employee.LastName));

            var options = new SalesQueryOptions
            {
                Includes = "Employee",
                OrderByDirection = builder.CurrentRoute.SortDirection,
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize
            };

            options.SortFilter(builder);

            var vm = new SalesListViewModel
            {
                Sales = data.Sales.List(options),
                Employees = data.Employees.List(new QueryOptions<Employee>
                {
                    OrderBy = e => e.LastName
                }),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Sales.Count),
                SalesListQuarter = new int[4] { 1, 2, 3, 4 },
                SalesListYear = data.Sales.List(new QueryOptions<Sales>
                {
                    Where = s => s.Year >= 2000
                }).Distinct().ToList(),
            };

            return View(vm);
        }

        [HttpPost]
        public RedirectToActionResult Filter(string[] filter, bool clear = false)
        {
            var builder = new SalesGridBuilder(HttpContext.Session);

            if (clear)
            {
                builder.ClearFilterSegments();
            }
            else
            {
                var employee = data.Employees.Get(filter[0].ToInt());
                builder.CurrentRoute.PageNumber = 1;
                builder.LoadFilterSegments(filter, employee);
            }

            builder.SaveRouteSegments();
            return RedirectToAction("Index", builder.CurrentRoute);
        }
    }
}
