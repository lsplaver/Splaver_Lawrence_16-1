using Microsoft.AspNetCore.Mvc;
using QuarterlySales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterlySales.Components
{
    public class YearDropDown : ViewComponent
    {
        public IViewComponentResult Invoke(string selectedValue)
        {
            List<int> years = new List<int>();
            int foundingYear = 1995;
            int maxYear = DateTime.Now.Year;
            for (int year = maxYear; year >= foundingYear; year--)
            {
                years.Add(year);
            }

            Dictionary<string, string> yearDictionary = new Dictionary<string, string>();
            for (int x = 0; x < years.Count(); x++)
            {
                yearDictionary.Add(years.ElementAt(x).ToString(), years.ElementAt(x).ToString());
            }

            DropDownViewModel vm = new DropDownViewModel
            {
                SelectedValue = selectedValue,
                DefaultValue = SalesGridDTO.DefaultFilter,
                Items = yearDictionary
            };

            return View(SharedPath.Select, vm);
        }
    }
}
