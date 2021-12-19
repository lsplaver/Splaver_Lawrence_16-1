using Microsoft.AspNetCore.Mvc;
using QuarterlySales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterlySales.Components
{
    public class EmployeeDropDown : ViewComponent
    {
        private IRepository<Employee> data { get; set; }
        public EmployeeDropDown(IRepository<Employee> rep) => data = rep;

        public IViewComponentResult Invoke(string selectedValue)
        {
            var employees = data.List(new QueryOptions<Employee>
            {
                OrderBy = e => e.FirstName
            });

            DropDownViewModel vm = new DropDownViewModel
            {
                SelectedValue = selectedValue,
                DefaultValue = SalesGridDTO.DefaultFilter,
                Items = employees.ToDictionary(
                    e => e.EmployeeId.ToString(), e => e.FullName)
            };

            return View(SharedPath.Select, vm);
        }
    }
}
