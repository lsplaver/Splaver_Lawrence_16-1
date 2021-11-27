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
        //public SalesContext context { get; set; }

        //public HomeController(SalesContext ctx) => context = ctx;

        private QuarterlySalesUnitOfWork data { get; set; }
        public HomeController(SalesContext ctx) => data = new QuarterlySalesUnitOfWork(ctx);

        [HttpGet]
        //public ViewResult Index(int id)
        //{
        //    IQueryable<Sales> query = context.Sales
        //        .Include(s => s.Employee)
        //        .OrderBy(s => s.Employee.LastName)
        //        .ThenBy(s => s.Employee.FirstName)
        //        .ThenBy(s => s.Year)
        //        .ThenBy(s => s.Quarter);

        //    if (id > 0)
        //    {
        //        query = query.Where(s => s.EmployeeId == id);
        //    }

        //    SalesListViewModel vm = new SalesListViewModel
        //    {
        //        Sales = query.ToList(),
        //        Employees = context.Employees.OrderBy(e => e.LastName).ThenBy(e => e.FirstName).ToList(),
        //        EmployeeId = id
        //    };

        //    return View(vm);
        //}
        public ViewResult Index(SalesGridDTO values)
        {
            var builder = new SalesGridBuilder(values, defaultSortField: nameof(Sales.Employee.FullName));

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
                    OrderBy = e => e.FullName
                }),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Sales.Count)
            };

            return View(vm);
        }

        [HttpPost]
        //public RedirectToActionResult Index(Employee employee)
        //{
        //    if (employee.EmployeeId > 0)
        //    {
        //        return RedirectToAction("Index", new { id = employee.EmployeeId });
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", new { id = string.Empty });
        //    }
        //}
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
